/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using System;

namespace Evolution.Framework
{
    /// <summary>
    /// Claim常量
    /// </summary>
    public static class OperatorModelClaimNames
    {
        public static string UserId
        {
            get { return "EF:UserId"; }
        }
        public static string UserCode
        {
            get { return "EF:UserCode"; }
        }
        public static string UserName
        {
            get { return "EF:UserName"; }
        }
        public static string CompanyId
        {
            get { return "EF:CompanyId"; }
        }
        public static string DepartmentId
        {
            get { return "EF:DepartmentId"; }
        }
        public static string RoleId
        {
            get { return "EF:RoleId"; }
        }
        public static string LoginIPAddress
        {
            get { return "EF:LoginIPAddress"; }
        }
        public static string LoginIPAddressName
        {
            get { return "EF:LoginIPAddressName"; }
        }
        public static string LoginToken
        {
            get { return "EF:LoginToken"; }
        }
        public static string LoginTime
        {
            get { return "EF:LoginTime"; }
        }
        public static string IsSystem
        {
            get { return "EF:IsSystem"; }
        }
        public static string RoleName
        {
            get { return "EF:RoleName"; }
        }
        public static string Permission
        {
            get { return "EF:Permission"; }
        }
    }

    /// <summary>
    /// 登录对象。用于保存登陆后获取到的用户信息
    /// </summary>
    public class LoginModel
    {
        public string UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string CompanyId { get; set; }
        public string DepartmentId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoginIPAddress { get; set; }
        public string LoginIPAddressName { get; set; }
        public string LoginToken { get; set; }
        public DateTime LoginTime { get; set; }
        public bool IsSystem { get; set; }
    }
}
