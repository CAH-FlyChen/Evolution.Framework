/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Framework;
using Evolution;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using Evolution.Web;
using Evolution.Data;
using Evolution.Data.Entity.SystemManage;
using Evolution.Application.SystemManage;

namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    [PermissionLevelDescrip("组织结构数据", "")]
    public class OrganizeController : EvolutionControllerBase
    {
        private OrganizeService organizeApp = null;
        public OrganizeController(OrganizeService organizeApp)
        {
            this.organizeApp = organizeApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        [PermissionLevelDescrip("获取组织结构选项数据", "获取组织结构选项数据")]
        public async Task<ActionResult> GetTreeSelectJson()
        {
            var data = await organizeApp.GetList();
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
        public async Task<ActionResult> GetTreeJson()
        {
            var data = await organizeApp.GetList();
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
        public async Task<ActionResult> GetTreeGridJson(string keyword)
        {
            var data = await organizeApp.GetList();

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
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await organizeApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [PermissionLevelDescrip("保存组织结构数据", "保存组织结构数据")]
        public async Task<IActionResult> SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            await organizeApp.Save(organizeEntity, keyValue, HttpContext);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        [PermissionLevelDescrip("删除组织结构数据", "删除组织结构数据")]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await organizeApp.Delete(keyValue);
            return Success("删除成功。");
        }
    }
}
