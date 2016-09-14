/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Framework;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Evolution.Data;

namespace NFine.Application.SystemManage
{
    public class UserLogOnApp
    {
        private IUserLogOnRepository service = null;
        private RoleApp roleApp = null;

        public UserLogOnApp(IUserLogOnRepository service, RoleApp roleApp)
        {
            this.service = service;
            this.roleApp = roleApp;
        }

        public void SignIn(OperatorModel om,HttpContext context)
        {
            //RoleEntity re = this.roleApp.GetForm(userEntity.F_RoleId);


            ClaimsIdentity identity = new ClaimsIdentity("local");
            //system
            identity.AddClaim(new Claim(ClaimTypes.Name, om.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, om.UserCode));
            identity.AddClaim(new Claim(ClaimTypes.Role, om.RoleName));
            //custom
            identity.AddClaim(new Claim(OperatorModelClaimNames.CompanyId, om.CompanyId));
            identity.AddClaim(new Claim(OperatorModelClaimNames.DepartmentId, om.DepartmentId));
            identity.AddClaim(new Claim(OperatorModelClaimNames.IsSystem, om.IsSystem.ToString()));
            identity.AddClaim(new Claim(OperatorModelClaimNames.LoginIPAddress, om.LoginIPAddress));
            identity.AddClaim(new Claim(OperatorModelClaimNames.LoginIPAddressName, om.LoginIPAddressName));
            identity.AddClaim(new Claim(OperatorModelClaimNames.LoginTime, om.LoginTime.ToString("yyyy-MM-dd HH:mm:ss")));
            identity.AddClaim(new Claim(OperatorModelClaimNames.LoginToken, om.LoginToken));
            identity.AddClaim(new Claim(OperatorModelClaimNames.RoleId, om.RoleId));
            identity.AddClaim(new Claim(OperatorModelClaimNames.UserCode, om.UserCode));
            identity.AddClaim(new Claim(OperatorModelClaimNames.UserId, om.UserId));
            identity.AddClaim(new Claim(OperatorModelClaimNames.UserName, om.UserName));
            identity.AddClaim(new Claim(OperatorModelClaimNames.RoleName, om.RoleName));

            ClaimsPrincipal cp = new ClaimsPrincipal(identity);
            //"Cookies",CookieAuthenticationDefaults.AuthenticationScheme
            context.Authentication.SignInAsync("CookieAuth", cp);

        }

        public void SignOut(HttpContext context)
        {
            context.Authentication.SignOutAsync("CookieAuth");
        }

        public UserLogOnEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void UpdateForm(UserLogOnEntity userLogOnEntity)
        {
            service.Update(userLogOnEntity);
        }
        public void RevisePassword(string userPassword,string keyValue)
        {
            UserLogOnEntity userLogOnEntity = new UserLogOnEntity();
            userLogOnEntity.Id = keyValue;
            userLogOnEntity.UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
            userLogOnEntity.UserPassword = Md5.md5(AESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
            service.Update(userLogOnEntity);
        }
    }
}
