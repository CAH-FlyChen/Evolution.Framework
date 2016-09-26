/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Application.SystemSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Application.SystemManage;
using Evolution.Application;
using Microsoft.AspNetCore.Mvc;
using Evolution.Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Evolution.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        UserApp userApp = null;
        LogApp logApp = null;
        UserLogOnApp logonApp = null;
        RoleApp roleApp = null;
        public LoginController(UserApp userapp, LogApp logApp, UserLogOnApp logonApp,RoleApp roleApp)
        {
            this.userApp = userapp;
            this.logApp = logApp;
            this.logonApp = logonApp;
            this.roleApp = roleApp;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            var test = string.Format("{0:E2}", 1);
            return View();
        }
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(HttpContext), @"image/Gif");
        }
        [HttpGet]
        public ActionResult OutLogin()
        {
            var userCode = HttpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserCode).Value;
            var userName = HttpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserName).Value;

            logApp.WriteDbLog(new LogEntity
            {
                ModuleName = "系统登录",
                Type = DbLogType.Exit.ToString(),
                Account = userCode,
                NickName = userName,
                Result = true,
                Description = "安全退出系统",
            }, HttpContext);
            //Session.Abandon();
            HttpContext.Session.Clear();
            logonApp.SignOut(HttpContext);
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CheckLogin(string username, string password, string code)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.ModuleName = "系统登录";
            logEntity.Type = DbLogType.Login.ToString();
            var currentContext = HttpContext;
            try
            {
                var verifyCodeInSession = WebHelper.GetSession("nfine_session_verifycode", currentContext);
                if (verifyCodeInSession.IsEmpty() ||  Md5.md5(code.ToLower(), 16) != verifyCodeInSession)
                {
                    throw new Exception("验证码错误，请重新输入");
                }
                UserEntity userEntity = userApp.CheckLogin(username, password);
                if (userEntity != null)
                {
                    var role = roleApp.GetForm(userEntity.RoleId);

                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = userEntity.Id;
                    operatorModel.UserCode = userEntity.Account;
                    operatorModel.UserName = userEntity.RealName;
                    operatorModel.CompanyId = userEntity.OrganizeId;
                    operatorModel.DepartmentId = userEntity.DepartmentId;
                    operatorModel.RoleId = userEntity.RoleId;
                    operatorModel.LoginIPAddress = Net.GetIp(HttpContext);
                    operatorModel.LoginIPAddressName = Net.GetLocation(operatorModel.LoginIPAddress);
                    operatorModel.LoginTime = DateTime.Now;
                    operatorModel.LoginToken = AESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    operatorModel.RoleName = role.FullName;
                    operatorModel.IsSystem = userEntity.Account == "admin";

                    logEntity.Account = userEntity.Account;
                    logEntity.NickName = userEntity.RealName;
                    logEntity.Result = true;
                    logEntity.Description = "登录成功";

                    logApp.WriteDbLog(logEntity, HttpContext);
                    logonApp.SignIn(operatorModel, HttpContext);
  
                }
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                logEntity.Account = username;
                logEntity.NickName = username;
                logEntity.Result = false;
                logEntity.Description = "登录失败，" + ex.Message;
                logApp.WriteDbLog(logEntity, currentContext);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }
    }
}
