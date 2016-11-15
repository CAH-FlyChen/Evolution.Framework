///*******************************************************************************
// * Copyright © 2016 Evolution.Framework 版权所有
// * Author: Evolution
// * Description: Evolution快速开发平台
// * Website：
//*********************************************************************************/
//using System.Collections.Generic;
//using System.Linq;
//using Evolution.Application.SystemManage;
//using Evolution.Domain.Entity.SystemManage;
//using Microsoft.AspNetCore.Mvc;
//using Evolution.Framework;

//namespace Evolution.Web.Areas.SystemManage.Controllers
//{
//    [Area("SystemManage")]
//    public class ModuleController : EvolutionControllerBase
//    {
//        private ModuleApp moduleApp = null;

//        public ModuleController(ModuleApp moduleApp)
//        {
//            this.moduleApp = moduleApp;
//        }

//        [HttpGet]
//        [HandlerAjaxOnly]
//        public ActionResult GetTreeSelectJson()
//        {
//            var data = moduleApp.GetList();
//            var treeList = new List<TreeSelectModel>();
//            foreach (ModuleEntity item in data)
//            {
//                TreeSelectModel treeModel = new TreeSelectModel();
//                treeModel.id = item.Id;
//                treeModel.text = item.FullName;
//                treeModel.parentId = item.ParentId;
//                treeList.Add(treeModel);
//            }
//            return Content(treeList.TreeSelectJson());
//        }
//        //[HttpGet]
//        //[HandlerAjaxOnly]
//        //public ActionResult GetTreeGridJson(string keyword)
//        //{
//        //    var data = moduleApp.GetList();
//        //    if (!string.IsNullOrEmpty(keyword))
//        //    {
//        //        data = data.TreeWhere(t => t.FullName.Contains(keyword));
//        //    }
//        //    var treeList = new List<TreeGridModel>();
//        //    foreach (ModuleEntity item in data)
//        //    {
//        //        TreeGridModel treeModel = new TreeGridModel();
//        //        bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
//        //        treeModel.id = item.Id;
//        //        treeModel.isLeaf = hasChildren;
//        //        treeModel.parentId = item.ParentId;
//        //        treeModel.expanded = hasChildren;
//        //        treeModel.entityJson = item.ToJson();
//        //        treeList.Add(treeModel);
//        //    }
//        //    return Content(treeList.TreeGridJson());
//        //}
//        //[HttpGet]
//        //[HandlerAjaxOnly]
//        //public ActionResult GetFormJson(string keyValue)
//        //{
//        //    var data = moduleApp.GetForm(keyValue);
//        //    return Content(data.ToJson());
//        //}
//        //[HttpPost]
//        //[HandlerAjaxOnly]
//        //[ValidateAntiForgeryToken]
//        //public ActionResult SubmitForm(ModuleEntity moduleEntity, string keyValue)
//        //{
//        //    moduleApp.SubmitForm(moduleEntity, keyValue,HttpContext);
//        //    return Success("操作成功。");
//        //}
//        //[HttpPost]
//        //[HandlerAjaxOnly]
//        ////[HandlerAuthorize]
//        //[ValidateAntiForgeryToken]
//        //public ActionResult DeleteForm(string keyValue)
//        //{
//        //    moduleApp.DeleteForm(keyValue);
//        //    return Success("删除成功。");
//        //}
//    }
//}
