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
using Evolution.Plugins.Area.Services;
using Evolution.Plugins.Area.IServices;
using Evolution.Plugins.Area.Entities;
using Evolution.Web;

namespace Evolution.Plugins.Area.Controllers
{
    [Area("SystemManage")]
    public class AreaController : EvolutionControllerBase
    {
        private IAreaService areaService = null;
        public AreaController(IAreaService areaService)
        {
            this.areaService = areaService;
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetTreeSelectJson()
        {
            var data = await areaService.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (AreaEntity item in data)
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
        public async Task<IActionResult> GetTreeGridJson(string keyword)
        {
            var data = await areaService.GetList();
            var treeList = new List<TreeGridModel>();
            foreach (AreaEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.text = item.FullName;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId;
                treeModel.expanded = true;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }
            return Content(treeList.TreeGridJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await areaService.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm(AreaEntity areaEntity, string keyValue)
        {
            await areaService.Save(areaEntity, keyValue, HttpContext);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await areaService.Delete(keyValue);
            return Success("删除成功。");
        }
    }
}
