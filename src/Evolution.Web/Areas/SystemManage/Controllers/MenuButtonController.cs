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

namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class MenuButtonController : ControllerBase
    {
        #region 私有变量
        private MenuApp menuApp = null;
        private MenuButtonApp menuButtonApp = null;
        #endregion
        #region 构造函数
        public MenuButtonController(MenuApp menuApp, MenuButtonApp moduleButtonApp)
        {
            this.menuApp = menuApp;
            this.menuButtonApp = moduleButtonApp;
        }
        #endregion
        /// <summary>
        /// 获取菜单按钮下拉选择框，选择上级使用。
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string menuId)
        {
            var data = menuButtonApp.GetListByMenuId(menuId);
            var treeList = new List<TreeSelectModel>();
            foreach (MenuButtonEntity item in data)
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
        /// 获取树形菜单按钮
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string menuId)
        {
            var data = menuButtonApp.GetListByMenuId(menuId);
            var treeList = new List<TreeGridModel>();
            foreach (MenuButtonEntity item in data)
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
        /// 获取菜单按钮
        /// </summary>
        /// <param name="keyValue">菜单按钮Id</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = menuButtonApp.GetMenuButtonById(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 保存菜单按钮
        /// </summary>
        /// <param name="entity">菜单按钮对象</param>
        /// <param name="keyValue">菜单按钮Id，无id则新建，有id则修改</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(MenuButtonEntity entity, string keyValue)
        {
            menuButtonApp.Save(entity, keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除菜单按钮
        /// </summary>
        /// <param name="keyValue">菜单按钮Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            menuButtonApp.Delete(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 克隆按钮的页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CloneButton()
        {
            return View();
        }
        /// <summary>
        /// 初始化按钮树状结构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCloneButtonTreeJson()
        {
            var menuData = menuApp.GetList();
            var buttonData = menuButtonApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (MenuEntity item in menuData)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = menuData.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.FullName;
                tree.value = item.EnCode;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                treeList.Add(tree);
            }
            foreach (MenuButtonEntity item in buttonData)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = buttonData.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.FullName;
                tree.value = item.EnCode;
                tree.parentId = item.ParentId == "0" ? item.MenuId : item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.hasChildren = hasChildren;
                if (item.Icon != "")
                {
                    tree.img = item.Icon;
                }
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        /// <summary>
        /// 保存克隆按钮
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="Ids">按钮Id字符串，用逗号分隔</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitCloneButton(string menuId, string Ids)
        {
            menuButtonApp.SaveCloneButton(menuId, Ids);
            return Success("克隆成功。");
        }
    }
}
