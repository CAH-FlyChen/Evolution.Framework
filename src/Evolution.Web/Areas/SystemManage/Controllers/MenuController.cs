/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using System.Collections.Generic;
using System.Linq;
using Evolution.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Mvc;
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Evolution.Web.Attributes;

namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class MenuController : EvolutionControllerBase
    {
        #region 私有变量
        private MenuService menuApp = null;
        private MenuButtonService menuButtonApp = null;
        private RoleAuthorizeService roleAuthorizeApp = null;
        private RoleService roleApp = null;
        #endregion
        #region 构造函数
        public MenuController(MenuService menuApp,MenuButtonService menuButtonApp, RoleAuthorizeService roleAuthorizeApp,RoleService roleApp)
        {
            this.menuApp = menuApp;
            this.menuButtonApp = menuButtonApp;
            this.roleAuthorizeApp = roleAuthorizeApp;
            this.roleApp = roleApp;
        }
        #endregion
        /// <summary>
        /// 根据角色Id获取系统菜单树内容，配置角色的菜单权限时使用
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetPermissionTree(string roleId)
        {
            var moduledata = await menuApp.GetList();
            var buttondata = await menuButtonApp.GetList();
            var authorizedata = new List<RoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = await roleAuthorizeApp.GetListByObjectId(roleId);
            }
            var treeList = new List<TreeViewModel>();
            foreach (MenuEntity item in moduledata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = moduledata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.FullName;
                tree.value = item.EnCode;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.ItemId == item.Id);
                tree.hasChildren = true;
                tree.img = item.Icon == "" ? "" : item.Icon;
                treeList.Add(tree);
            }
            foreach (MenuButtonEntity item in buttondata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = buttondata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.FullName;
                tree.value = item.EnCode;
                tree.parentId = item.ParentId == "0" ? item.MenuId : item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.ItemId == item.Id);
                tree.hasChildren = hasChildren;
                tree.img = item.Icon == "" ? "" : item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        /// <summary>
        /// 保存菜单权限
        /// </summary>
        /// <param name="roleEntity">角色对象</param>
        /// <param name="menuIds">选中的菜单Id字符串，逗号分隔</param>
        /// <param name="keyValue">角色Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveMenuPermission(RoleEntity roleEntity, string menuIds, string keyValue)
        {
            await roleApp.Save(roleEntity, menuIds.Split(','), keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 获取界面上下拉选择框内容，供界面选择上级菜单使用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetTreeSelectJson()
        {
            var data = await menuApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (MenuEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.FullName;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 获取菜单树状列表
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetTreeGridJson(string keyword)
        {
            var data = await menuApp.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FullName.Contains(keyword));
            }
            var treeList = new List<TreeGridModel>();
            foreach (MenuEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        /// <summary>
        /// 获取菜单对象
        /// </summary>
        /// <param name="keyValue">菜单Id</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await menuApp.GetMenuById(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="menuEntity">菜单对象</param>
        /// <param name="keyValue">菜单Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm(MenuEntity menuEntity, string keyValue)
        {
            await menuApp.Save(menuEntity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除菜单，若有父菜单，则禁止删除
        /// </summary>
        /// <param name="keyValue">菜单项目Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await menuApp.Delete(keyValue);
            return Success("删除成功。");
        }
    }
}
