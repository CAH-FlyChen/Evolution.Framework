/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
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
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string moduleId)
        {
            var data = menuButtonApp.GetListByMenuId(moduleId);
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
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string moduleId)
        {
            var data = menuButtonApp.GetListByMenuId(moduleId);
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
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = menuButtonApp.GetItemById(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(MenuButtonEntity entity, string keyValue)
        {
            menuButtonApp.Save(entity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            menuButtonApp.DeleteItem(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult CloneButton()
        {
            return View();
        }
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
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitCloneButton(string moduleId, string Ids)
        {
            menuButtonApp.SubmitCloneButton(moduleId, Ids);
            return Success("克隆成功。");
        }
    }
}
