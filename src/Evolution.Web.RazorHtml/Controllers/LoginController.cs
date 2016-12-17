/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Domain.Entity.SystemSecurity;
using System;
using System.Linq;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Mvc;
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using System.Net.Http;
using System.Collections.Generic;
using JWT.Common;
using System.Security.Claims;
using Newtonsoft.Json;
using Evolution.IInfrastructure;

namespace Evolution.Web.Controllers
{
    /// <summary>
    /// 登录Controller
    /// </summary>
    [AllowAnonymous]
    public class LoginController : Controller
    {
        HttpHelper httpHelper;
        public LoginController(HttpHelper httpHelper)
        {
            this.httpHelper = httpHelper;
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Index()
        {
            var test = string.Format("{0:E2}", 1);
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAuthCode()
        {
            VerifyCode vc = new VerifyCode();
            byte[] content = await vc.GetVerifyCode(HttpContext);
            return File(content, @"image/Gif");
        }
        /// <summary>
        /// 登出系统
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutLogin()
        {
            //var userCode = HttpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserId).Value;
            //var userName = HttpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserName).Value;

            //logApp.WriteDbLog(new LogEntity
            //{
            //    ModuleName = "系统登录",
            //    Type = DbLogType.Exit.ToString(),
            //    Account = userCode,
            //    NickName = userName,
            //    Result = true,
            //    Description = "安全退出系统",
            //}, HttpContext);
            //Session.Abandon();
            HttpContext.Session.Clear();
            //logonApp.SignOut(HttpContext);
            return RedirectToAction("Index", "Login");
        }

        /// <summary>
        /// 用jwt校验并写入cookie
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<ActionResult> CheckLoginJwt(string username,string password,string code)
        {
            try
            {
                //验证用户名密码
                //arrange
                var data = new Dictionary<string, string>();
                data.Add("username", username);
                data.Add("password", password);

                string res = await httpHelper.HttpPost("/auth/token", data);
                Token token = JsonConvert.DeserializeObject<Token>(res);

                ClaimsIdentity identity = new ClaimsIdentity("local");
                //system
                identity.AddClaim(new Claim(ClaimTypes.Name, token.user_name));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, token.user_code));
                identity.AddClaim(new Claim(ClaimTypes.Role, token.role_name));
                identity.AddClaim(new Claim(OperatorModelClaimNames.Token, token.access_token));
                
                ClaimsPrincipal cp = new ClaimsPrincipal(identity);
                //"Cookies",CookieAuthenticationDefaults.AuthenticationScheme
                await HttpContext.Authentication.SignInAsync("CookieAuth", cp);
                HttpContext.Response.Cookies.Append("access_token", token.access_token);
                HttpContext.Response.Cookies.Append("token_refresh_time", token.expires_dt.ToString("yyyy-MM-dd HH:mm:ss"));

                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

        #region 私有方法
        /// <summary>
        /// 创建登录对象
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <param name="roleEntity">角色实体</param>
        /// <returns>登录对象</returns>
        private LoginModel CreateLoginModel(UserEntity userEntity, RoleEntity roleEntity)
        {
            LoginModel model = new LoginModel();
            model.UserId = userEntity.Id;
            model.UserCode = userEntity.Account;
            model.UserName = userEntity.RealName;
            model.CompanyId = userEntity.OrganizeId;
            model.DepartmentId = userEntity.DepartmentId;
            model.RoleId = userEntity.RoleId;
            model.LoginIPAddress = Net.GetIp(HttpContext);
            model.LoginIPAddressName = Net.GetLocation(model.LoginIPAddress);
            model.LoginTime = DateTime.Now;
            model.LoginToken = AESEncrypt.Encrypt(Guid.NewGuid().ToString());
            model.RoleName = roleEntity.FullName;
            model.IsSystem = userEntity.Account == "admin";
            return model;
        }
        #endregion
    }
}
