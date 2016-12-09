/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Mvc;
using Evolution.Framework;
using System.Threading.Tasks;
using Evolution.Web.Attributes;

namespace Evolution.Web.API.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class RoleController : EvolutionControllerBase
    {
        #region 私有变量
        private RoleApp roleApp = null;
        private RoleAuthorizeApp roleAuthorizeApp = null;
        private MenuButtonApp moduleButtonApp = null;
        #endregion
        #region 构造函数
        public RoleController(RoleApp roleApp, RoleAuthorizeApp roleAuthorizeApp, MenuButtonApp moduleButtonApp)
        {
            this.roleApp = roleApp;
            this.roleAuthorizeApp = roleAuthorizeApp;
            this.moduleButtonApp = moduleButtonApp;
        }
        #endregion
        /// <summary>
        /// 获取角色表格数据
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetGridJson(string keyword)
        {
            var data = await roleApp.GetList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取角色表单数据
        /// </summary>
        /// <param name="keyValue">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetFormJson(string keyValue)
        {
            var data = await roleApp.GetRoleById(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 提交角色表单数据
        /// </summary>
        /// <param name="roleEntity">角色对象</param>
        /// <param name="permissionIds">授权菜单Id列表</param>
        /// <param name="keyValue">角色Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitForm(RoleEntity roleEntity, string permissionIds, string keyValue)
        {
            await roleApp.Save(roleEntity, permissionIds.Split(','), keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除角色及相关授权
        /// </summary>
        /// <param name="keyValue">角色Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await roleApp.Delete(keyValue);
            return Success("删除成功。");
        }
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
