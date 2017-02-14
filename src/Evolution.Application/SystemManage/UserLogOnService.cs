/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Evolution.Data;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Evolution.Application.SystemManage
{
    public class UserLogOnService : IUserLogOnService
    {
        private IUserLogOnRepository service = null;
        private RoleService roleApp = null;
        private RoleAuthorizeService roleAuth = null;
        private MenuButtonService mb = null;

        public UserLogOnService(IUserLogOnRepository service, RoleService roleApp,RoleAuthorizeService roleAuth,MenuButtonService mb)
        {
            this.service = service;
            this.roleApp = roleApp;
            this.roleAuth = roleAuth;
            this.mb = mb;
        }

        public void SignIn(LoginModel om,HttpContext context)
        {
            //RoleEntity re = this.roleApp.GetForm(userEntity.F_RoleId);

            ClaimsIdentity identity = new ClaimsIdentity("local");
            //system
            identity.AddClaim(new Claim(ClaimTypes.Name, om.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, om.UserCode));
            identity.AddClaim(new Claim(ClaimTypes.Role, om.RoleName));
            //custom
            //identity.AddClaim(new Claim(OperatorModelClaimNames.CompanyId, om.CompanyId));
            //identity.AddClaim(new Claim(OperatorModelClaimNames.DepartmentId, om.DepartmentId));
            identity.AddClaim(new Claim(OperatorModelClaimNames.IsSystem, om.IsSystem.ToString()));
            //identity.AddClaim(new Claim(OperatorModelClaimNames.LoginIPAddress, om.LoginIPAddress));
            //identity.AddClaim(new Claim(OperatorModelClaimNames.LoginIPAddressName, om.LoginIPAddressName));
            //identity.AddClaim(new Claim(OperatorModelClaimNames.LoginTime, om.LoginTime.ToString("yyyy-MM-dd HH:mm:ss")));
            //identity.AddClaim(new Claim(OperatorModelClaimNames.LoginToken, om.LoginToken));
            //identity.AddClaim(new Claim(OperatorModelClaimNames.RoleId, om.RoleId));
            //identity.AddClaim(new Claim(OperatorModelClaimNames.UserCode, om.UserCode));
            identity.AddClaim(new Claim(OperatorModelClaimNames.UserId, om.UserId));
            identity.AddClaim(new Claim(OperatorModelClaimNames.UserName, om.UserName));
            identity.AddClaim(new Claim(OperatorModelClaimNames.RoleName, om.RoleName));
            identity.AddClaim(new Claim(OperatorModelClaimNames.TenantId, om.TenantId));

            identity.AddClaim(new Claim(OperatorModelClaimNames.Permission,JsonConvert.SerializeObject(roleAuth.GetResorucePermissionsByRoleId(om.RoleId))));

            ClaimsPrincipal cp = new ClaimsPrincipal(identity);
            //"Cookies",CookieAuthenticationDefaults.AuthenticationScheme
            context.Authentication.SignInAsync("CookieAuth", cp);

        }

        public void SignOut(HttpContext context)
        {
            context.Authentication.SignOutAsync("CookieAuth");
        }

        public Task<UserLogOnEntity> GetForm(string keyValue)
        {
            return service.FindEntityAsync(keyValue);
        }
        public Task<int> UpdateForm(UserLogOnEntity userLogOnEntity)
        {
            return service.UpdateAsync(userLogOnEntity);
        }
        public Task<int> RevisePassword(string userPassword,string keyValue)
        {
            UserLogOnEntity userLogOnEntity = new UserLogOnEntity();
            userLogOnEntity.Id = keyValue;
            userLogOnEntity.UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
            userLogOnEntity.UserPassword = Md5.md5(AESEncrypt.Encrypt(userPassword.ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
            return service.UpdateAsync(userLogOnEntity);
        }
    }
}
