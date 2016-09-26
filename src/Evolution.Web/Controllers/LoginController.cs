﻿/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Application.SystemSecurity;
using System;
using System.Linq;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Application.SystemManage;
using Evolution.Application;
using Microsoft.AspNetCore.Mvc;
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Evolution.Web.Controllers
{
    /// <summary>
    /// 登录Controller
    /// </summary>
    [AllowAnonymous]
    public class LoginController : Controller
    {
        #region 私有变量
        UserApp userApp = null;
        LogApp logApp = null;
        UserLogOnApp logonApp = null;
        RoleApp roleApp = null;
        #endregion
        #region 构造函数
        public LoginController(UserApp userapp, LogApp logApp, UserLogOnApp logonApp,RoleApp roleApp)
        {
            this.userApp = userapp;
            this.logApp = logApp;
            this.logonApp = logonApp;
            this.roleApp = roleApp;
        }
        #endregion

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
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(HttpContext), @"image/Gif");
        }
        /// <summary>
        /// 登出系统
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CheckLogin(string username, string password, string code)
        {
            //初始化登录日志
            LogEntity logEntity = new LogEntity();
            logEntity.ModuleName = "系统登录";
            logEntity.Type = DbLogType.Login.ToString();
            try
            {
                //验证 '验证码'
                var verifyCodeInSession = WebHelper.GetSession("evolution_session_verifycode", HttpContext);
                if (verifyCodeInSession.IsEmpty() || Md5.md5(code.ToLower(), 16) != verifyCodeInSession)
                    throw new Exception("验证码错误，请重新输入！");
                //验证用户名密码
                UserEntity userEntity = userApp.CheckLogin(username, password);
                var role = roleApp.GetRoleById(userEntity.RoleId);
                //设置登录对象
                LoginModel operatorModel = CreateLoginModel(userEntity, role);
                //写入登录日志
                logEntity.Account = userEntity.Account;
                logEntity.NickName = userEntity.RealName;
                logEntity.Result = true;
                logEntity.Description = "登录成功";
                logApp.WriteDbLog(logEntity, HttpContext);
                //登录
                logonApp.SignIn(operatorModel, HttpContext);

                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                logEntity.Account = username;
                logEntity.NickName = username;
                logEntity.Result = false;
                logEntity.Description = "登录失败，" + ex.Message;
                logApp.WriteDbLog(logEntity, HttpContext);
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
