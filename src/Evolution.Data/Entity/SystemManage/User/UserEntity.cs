/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Data.Entity;
using System;

namespace Evolution.Domain.Entity.SystemManage
{
    public class UserEntity : EntityBase
    {
        public string Account { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string HeadIcon { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string WeChat { get; set; }
        public string ManagerId { get; set; }
        public int? SecurityLevel { get; set; }
        public string Signature { get; set; }
        public string OrganizeId { get; set; }
        public string DepartmentId { get; set; }
        public string RoleId { get; set; }
        public string DutyId { get; set; }
        public bool? IsAdministrator { get; set; }
    }
}
