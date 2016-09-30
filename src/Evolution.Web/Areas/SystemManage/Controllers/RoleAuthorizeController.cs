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
using System.Threading.Tasks;

namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class RoleAuthorizeController : ControllerBase
    {
        #region 私有变量
        private RoleAuthorizeApp roleAuthorizeApp = null;
        private MenuButtonApp moduleButtonApp = null;
        private RoleApp roleApp = null;
        private ResourceApp resourceApp = null;
        #endregion
        #region 构造函数
        public RoleAuthorizeController(RoleAuthorizeApp roleAuthorizeApp,  MenuButtonApp moduleButtonApp,RoleApp roleApp,ResourceApp resourceApp)
        {
            this.roleAuthorizeApp = roleAuthorizeApp;
            this.moduleButtonApp = moduleButtonApp;
            this.roleApp = roleApp;
            this.resourceApp = resourceApp;
        }
        #endregion

        /// <summary>
        /// 获取并初始化授权树
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetResourceTree(string roleId)
        {
            var resourcedata = await resourceApp.GetList();
            var authorizedata = new List<RoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = await roleAuthorizeApp.GetListByObjectId(roleId);
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
                tree.checkstate = authorizedata.Count(t => t.ItemId == item.ClientID);
                tree.hasChildren = hasChildren;
                //tree.img = item.Icon == "" ? "" : item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        /// <summary>
        /// 保存角色资源授权
        /// </summary>
        /// <param name="data">授权对象，key为md5的Url值，value为是否选中，默认均为选中，可随意。</param>
        /// <param name="keyValue">角色Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitResourceForm(Dictionary<string,string> data,string keyValue)
        {
            List<string> keys = new List<string>();
            foreach(var d in data)
            {
                if (d.Key == "FullName" || d.Key== "EnCode" || d.Key == "Id") continue;
                keys.Add(d.Key);
            }
            await roleAuthorizeApp.Save(keyValue, keys);
            return Success("操作成功。");
        }
        /// <summary>
        /// 获取角色资源表单
        /// </summary>
        /// <param name="keyValue">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            string permissionIds = "";
            var data = await roleAuthorizeApp.GetResoucesByRoleId(keyValue,out permissionIds);
            var rData = new
            {
                Id = keyValue,
                EnCode = data.EnCode,
                FullName = data.FullName,
                PermissionIds = permissionIds
            };
            return Content(rData.ToJson());
        }
        /// <summary>
        /// 获取角色资源表格
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        //[HttpGet]
        //[HandlerAjaxOnly]
        //public ActionResult GetGridJson(string keyword)
        //{
        //    var data = roleApp.GetList(keyword);
        //    return Content(data.ToJson());
        //}
    }
}
