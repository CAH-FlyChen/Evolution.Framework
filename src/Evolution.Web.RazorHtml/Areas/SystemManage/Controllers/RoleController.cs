/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Mvc;
using Evolution.Framework;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    [Authorize]
    public class RoleController : EvolutionControllerBase
    {
        /// <summary>
        /// 角色资源授权页面
        /// </summary>
        /// <param name="keyValue">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Permission(string keyValue)
        {
            return View();
        }
        /// <summary>
        /// 角色菜单授权界面
        /// </summary>
        /// <param name="keyValue">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MenuPermission(string keyValue)
        {
            return View();
        }
    }
}
