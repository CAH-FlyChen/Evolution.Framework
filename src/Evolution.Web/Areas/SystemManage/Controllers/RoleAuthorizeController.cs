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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using Evolution;

namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class RoleAuthorizeController : ControllerBase
    {
        private RoleAuthorizeApp roleAuthorizeApp = null;
        private ModuleApp moduleApp = null;
        private MenuButtonApp moduleButtonApp = null;
        private RoleApp roleApp = null;
        private ResourceApp resourceApp = null;

        public RoleAuthorizeController(RoleAuthorizeApp roleAuthorizeApp, ModuleApp moduleApp, MenuButtonApp moduleButtonApp,RoleApp roleApp,ResourceApp resourceApp)
        {
            this.roleAuthorizeApp = roleAuthorizeApp;
            this.moduleApp = moduleApp;
            this.moduleButtonApp = moduleButtonApp;
            this.roleApp = roleApp;
            this.resourceApp = resourceApp;
        }


        /// <summary>
        /// 获取并初始化授权树
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ActionResult GetResourceTree(string roleId)
        {
            var resourcedata = resourceApp.GetList();
            var authorizedata = new List<RoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = roleAuthorizeApp.GetListByObjectId(roleId);
            }
            var treeList = new List<TreeViewModel>();
            foreach (ResourceEntity item in resourcedata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = resourcedata.Count(t => t.ParentClientId == item.ClientID) == 0 ? false : true;
                tree.id = item.ClientID;
                tree.text = item.Name.Replace("Controller","");
                tree.value = item.ClientID;
                tree.parentId = item.ParentClientId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.ItemId == item.ClientID
                    );
                tree.hasChildren = hasChildren;
                //tree.img = item.Icon == "" ? "" : item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        /// <summary>
        /// 用于保存内容
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        //[HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Dictionary<string,string> data,string keyValue)
        {
            List<string> keys = new List<string>();
            foreach(var d in data)
            {
                if (d.Key == "FullName" || d.Key== "EnCode" || d.Key == "Id") continue;
                keys.Add(d.Key);
            }

            roleAuthorizeApp.Save(keyValue, keys,HttpContext);

            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            string permissionIds = "";
            var data = roleAuthorizeApp.GetForm(keyValue,out permissionIds);
            var rData = new
            {
                Id = keyValue,
                EnCode = data.EnCode,
                FullName = data.FullName,
                PermissionIds = permissionIds
            };
            return Content(rData.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = roleApp.GetList(keyword);
            return Content(data.ToJson());
        }
    }
}
