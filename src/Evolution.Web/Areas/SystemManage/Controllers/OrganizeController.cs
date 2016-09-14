/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using NFine.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Framework;
using Evolution;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    [PermissionLevelDescrip("组织结构数据","")]
    public class OrganizeController : ControllerBase
    {
        private OrganizeApp organizeApp = null;

        public OrganizeController(OrganizeApp organizeApp)
        {
            this.organizeApp = organizeApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        [PermissionLevelDescrip("获取组织结构选项数据", "获取组织结构选项数据")]
        public ActionResult GetTreeSelectJson()
        {
            var data = organizeApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (OrganizeEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.FullName;
                treeModel.parentId = item.ParentId;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        [PermissionLevelDescrip("获取组织结构数据树状列表", "获取组织结构数据树状列表")]
        public ActionResult GetTreeJson()
        {
            var data = organizeApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (OrganizeEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.FullName;
                tree.value = item.EnCode;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        [PermissionLevelDescrip("获取组织结构数据列表", "获取组织结构数据列表")]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = organizeApp.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FullName.Contains(keyword));
            }
            var treeList = new List<TreeGridModel>();
            foreach (OrganizeEntity item in data)
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
        [PermissionLevelDescrip("获取一个组织结构数据", "获取一个组织结构数据")]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = organizeApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [PermissionLevelDescrip("保存组织结构数据", "保存组织结构数据")]
        public ActionResult SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            organizeApp.SubmitForm(organizeEntity, keyValue,HttpContext);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        [PermissionLevelDescrip("删除组织结构数据", "删除组织结构数据")]
        public ActionResult DeleteForm(string keyValue)
        {
            organizeApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
