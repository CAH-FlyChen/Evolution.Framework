//// * Copyright © 2016 Evolution.Framework 版权所有
//// * Author: Evolution
//// * Description: Evolution快速开发平台
//// * Website：
////*********************************************************************************/
//using Microsoft.AspNetCore.Http;
/////*******************************************************************************
//namespace Evolution.Framework
//{
//    public class OperatorProvider
//    {
//        public static OperatorProvider Provider
//        {
//            get { return new OperatorProvider(); }
//        }
//        private string LoginUserKey = "nfine_loginuserkey_2016";
//        private string LoginProvider = "";//Configs.GetValue("LoginProvider");

//        public OperatorModel GetCurrent(HttpContext httpContext)
//        {
//            OperatorModel operatorModel = new OperatorModel();
//            if (LoginProvider == "Cookie")
//            {
//                operatorModel = AESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey, httpContext).ToString()).ToObject<OperatorModel>();
//            }
//            else
//            {
//                operatorModel = AESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey, httpContext)).ToObject<OperatorModel>();
//            }
//            return operatorModel;
//        }
//        //public void AddCurrent(OperatorModel operatorModel,HttpContext context)
//        //{
//        //    if (LoginProvider == "Cookie")
//        //    {
//        //        WebHelper.WriteCookie(LoginUserKey, AESEncrypt.Encrypt(operatorModel.ToJson()), 60, context);
//        //    }
//        //    else
//        //    {
//        //        WebHelper.WriteSession(LoginUserKey, AESEncrypt.Encrypt(operatorModel.ToJson()),context);
//        //    }
//        //    //不支持mac地址获取
//        //    WebHelper.WriteCookie("nfine_mac", Md5.md5(Net.GetMacByNetworkInterface().ToJson(), 32), context);
//        //    WebHelper.WriteCookie("nfine_licence", Licence.GetLicence(), context);
//        //}
//        public void RemoveCurrent(HttpContext context)
//        {
//            if (LoginProvider == "Cookie")
//            {
//                WebHelper.RemoveCookie(LoginUserKey.Trim(), context);
//            }
//            else
//            {
//                WebHelper.RemoveSession(LoginUserKey.Trim());
//            }
//        }
//    }
//}
