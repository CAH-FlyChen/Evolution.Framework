/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/


using Evolution.Plugins.WeiXin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using System;
using System.IO;
using Senparc.Weixin.MP.MvcExtension;
using Evolution.Plugins.WeiXin.IServices;
using System.Collections.Generic;
using Evolution.Plugins.WeiXin.Entities;
using System.Linq;
using Evolution.Web;
using Microsoft.AspNetCore.Authorization;
using Evolution.Framework;
using System.Xml;
using System.Xml.Linq;

namespace Evolution.Plugins.Area.Controllers
{
    [Area("WeiXin")]
    [AllowAnonymous]
    public class WXPController : Controller
    {
        public static List<WeiXinConfigEntity> WXConfigs = null;

        public string Token {
            get
            {
                return WXConfigs.Single(t => t.TenantId == this.TenantId).Token;
            }
        }//与微信公众账号后台的Token设置保持一致，区分大小写。
        public string EncodingAESKey {
            get
            {
                return WXConfigs.Single(t => t.TenantId == this.TenantId).EncodingAESKey;
            }
        }//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public string AppId {
            get
            {
                return WXConfigs.Single(t => t.TenantId == this.TenantId).AppId;
            }
        }//与微信公众账号后台的AppId设置保持一致，区分大小写。

        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }

        public string TenantId
        {
            get
            {
                return HttpContext.Request.Query["tenantId"];
            }
        }
        public string UserId
        {
            get
            {
                return HttpContext.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.UserId).Value;
            }
        }
        public string IsSystem
        {
            get
            {
                return HttpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.IsSystem).Value;
            }
        }

        readonly Func<string> _getRandomFileName = () => DateTime.Now.ToString("yyyyMMdd-HHmmss") + Guid.NewGuid().ToString("n").Substring(0, 6);
        public string rootPath = string.Empty;
        public IWeiXinConfigService service = null;
        public WXPController(IHostingEnvironment env,IWeiXinConfigService service)
        {
            rootPath = env.ContentRootPath;
            this.service = service;
            if(WXConfigs==null)
                WXConfigs = service.GetList().Result;
        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://sdk.weixin.senparc.com/weixin
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// PS：此方法为简化方法，效果与OldPost一致。
        /// v0.8之后的版本可以结合Senparc.Weixin.MP.MvcExtension扩展包，使用WeixinResult，见MiniPost方法。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content("参数错误！");
            }

            postModel.Token = Token;//根据自己后台的设置保持一致
            postModel.EncodingAESKey = this.EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = AppId;//根据自己后台的设置保持一致

            //v4.2.2之后的版本，可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
            var maxRecordCount = 10;

            var logPath = string.Format("{1}/App_Data/MP/{0}/", DateTime.Now.ToString("yyyy-MM-dd"), rootPath);
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            var stream = this.HttpContext.Request.Body;
            XDocument doc = XDocument.Load(stream);
            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new CustomMessageHandler(doc, postModel, maxRecordCount);


            try
            {
                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                string filePath = Path.Combine(logPath, string.Format("{0}_Request_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName));
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
                messageHandler.RequestDocument.Save(fs);
                if (messageHandler.UsingEcryptMessage)
                {
                    FileStream fsx = new FileStream(Path.Combine(logPath, string.Format("{0}_Request_Ecrypt_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName)), FileMode.OpenOrCreate);
                    messageHandler.EcryptRequestDocument.Save(fsx);
                }

                /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
                 * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
                messageHandler.OmitRepeatedMessage = true;


                //执行微信处理过程
                messageHandler.Execute();

                //测试时可开启，帮助跟踪数据

                //if (messageHandler.ResponseDocument == null)
                //{
                //    throw new Exception(messageHandler.RequestDocument.ToString());
                //}

                if (messageHandler.ResponseDocument != null)
                {
                    FileStream fsa = new FileStream(Path.Combine(logPath, string.Format("{0}_Response_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName)), FileMode.CreateNew);
                    messageHandler.ResponseDocument.Save(fsa);
                }

                if (messageHandler.UsingEcryptMessage)
                {
                    FileStream fsb = new FileStream(Path.Combine(logPath, string.Format("{0}_Response_Final_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName)), FileMode.CreateNew);
                    //记录加密后的响应信息
                    messageHandler.FinalResponseDocument.Save(fsb);
                }

                //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
                //return new FixWeixinBugWeixinResult(messageHandler);//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
                return new WeixinResult(messageHandler);//v0.8+
            }
            catch (Exception ex)
            {
                FileStream fsc = new FileStream(rootPath + "/App_Data/Error_" + _getRandomFileName() + ".txt", FileMode.CreateNew);
                using (TextWriter tw = new StreamWriter(fsc))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }

                    if (ex.InnerException != null)
                    {
                        tw.WriteLine("========= InnerException =========");
                        tw.WriteLine(ex.InnerException.Message);
                        tw.WriteLine(ex.InnerException.Source);
                        tw.WriteLine(ex.InnerException.StackTrace);
                    }

                    tw.Flush();
                }
                return Content("");
            }
        }


        /// <summary>
        /// 最简化的处理流程（不加密）
        /// </summary>
        [HttpPost]
        [ActionName("MiniPost")]
        public ActionResult MiniPost(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                //return Content("参数错误！");//v0.7-
                return new WeixinResult("参数错误！");//v0.8+
            }

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = AppId;//根据自己后台的设置保持一致

            var stream = this.HttpContext.Request.Body;
            XDocument doc = XDocument.Load(stream);

            var messageHandler = new CustomMessageHandler(doc, postModel, 10);

            messageHandler.Execute();//执行微信处理过程

            //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
            return new FixWeixinBugWeixinResult(messageHandler);//v0.8+
            //return new WeixinResult(messageHandler);//v0.8+
        }

        /*
         * v0.3.0之前的原始Post方法见：WeixinController_OldPost.cs
         *
         * 注意：虽然这里提倡使用CustomerMessageHandler的方法，但是MessageHandler基类最终还是基于OldPost的判断逻辑，
         * 因此如果需要深入了解Senparc.Weixin.MP内部处理消息的机制，可以查看WeixinController_OldPost.cs中的OldPost方法。
         * 目前为止OldPost依然有效，依然可用于生产。
         */

        /// <summary>
        /// 为测试并发性能而建
        /// </summary>
        /// <returns></returns>
        //public ActionResult ForTest()
        //{
        //异步并发测试（提供给单元测试使用）
        //DateTime begin = DateTime.Now;
        //int t1, t2, t3;
        //System.Threading.ThreadPool.GetAvailableThreads(out t1, out t3);
        //System.Threading.ThreadPool.GetMaxThreads(out t2, out t3);
        //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
        //DateTime end = DateTime.Now;
        //var thread = System.Threading.Thread.CurrentThread;
        //var result = string.Format("TId:{0}\tApp:{1}\tBegin:{2:mm:ss,ffff}\tEnd:{3:mm:ss,ffff}\tTPool：{4}",
        //        thread.ManagedThreadId,
        //        HttpContext.ApplicationInstance.GetHashCode(),
        //        begin,
        //        end,
        //        t2 - t1
        //        );
        //return Content(result);
        //}
    }
}
