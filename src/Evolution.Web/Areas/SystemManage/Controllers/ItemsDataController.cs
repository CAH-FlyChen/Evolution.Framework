/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/

using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Evolution.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;


namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class ItemsDataController : ControllerBase
    {
        private ItemsDetailApp itemsDetailApp = null;

        public ItemsDataController(ItemsDetailApp itemsDetailApp)
        {
            this.itemsDetailApp = itemsDetailApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = itemsDetailApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            var data = itemsDetailApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (ItemsDetailEntity item in data)
            {
                list.Add(new { id = item.ItemCode, text = item.ItemName });
            }
            return Content(list.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = itemsDetailApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            itemsDetailApp.SubmitForm(itemsDetailEntity, keyValue,HttpContext);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            itemsDetailApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
