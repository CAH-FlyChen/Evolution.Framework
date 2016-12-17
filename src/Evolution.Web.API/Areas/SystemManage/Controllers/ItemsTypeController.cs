/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Evolution.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Web.Attributes;

namespace Evolution.Web.API.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class ItemsTypeController : EvolutionControllerBase
    {
        private ItemsService itemsApp = null;

        public ItemsTypeController(ItemsService itemsApp)
        {
            this.itemsApp = itemsApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetTreeSelectJson()
        {
            var data = await itemsApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (ItemsEntity item in data)
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
        public async Task<IActionResult> GetTreeJson()
        {
            var data = await itemsApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (ItemsEntity item in data)
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
        public async Task<IActionResult> GetTreeGridJson()
        {
            var data = await itemsApp.GetList();
            var treeList = new List<TreeGridModel>();
            foreach (ItemsEntity item in data)
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
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await itemsApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm(ItemsEntity itemsEntity, string keyValue)
        {
            await itemsApp.Save(itemsEntity, keyValue,HttpContext);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await itemsApp.Delete(keyValue);
            return Success("删除成功。");
        }
    }
}
