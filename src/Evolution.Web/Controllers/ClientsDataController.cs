/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using NFine.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;

namespace NFine.Web.Controllers
{
    //[HandlerLogin]
    public class ClientsDataController : Controller
    {
        private ItemsDetailApp itemDetailApp = null;
        private ItemsApp itemsApp = null;
        private OrganizeApp organizeApp = null;
        private RoleApp roleApp = null;
        private DutyApp dutyApp = null;
        private RoleAuthorizeApp roleAuthorizeApp = null;
        public ClientsDataController(ItemsDetailApp itemDetailApp, ItemsApp itemsApp,OrganizeApp organizeApp, RoleApp roleApp, DutyApp dutyApp, RoleAuthorizeApp roleAuthorizeApp)
        {
            this.itemDetailApp = itemDetailApp;
            this.itemsApp = itemsApp;
            this.organizeApp = organizeApp;
            this.roleApp = roleApp;
            this.dutyApp = dutyApp;
            this.roleAuthorizeApp = roleAuthorizeApp;
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                dataItems = this.GetDataItemList(),
                organize = this.GetOrganizeList(),
                role = this.GetRoleList(),
                duty = this.GetDutyList(),
                user = "",
                authorizeMenu = this.GetMenuList(),
                authorizeButton = this.GetMenuButtonList(),
            };
            return Content(data.ToJson());
        }
        private object GetDataItemList()
        {
            var itemdata = itemDetailApp.GetList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in itemsApp.GetList())
            {
                var dataItemList = itemdata.FindAll(t => t.ItemId.Equals(item.Id));
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.ItemCode, itemList.ItemName);
                }
                dictionaryItem.Add(item.EnCode, dictionaryItemList);
            }
            return dictionaryItem;
        }
        private object GetOrganizeList()
        {
            var data = this.organizeApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (OrganizeEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.EnCode,
                    fullname = item.FullName
                };
                dictionary.Add(item.Id, fieldItem);
            }
            return dictionary;
        }
        private object GetRoleList()
        {
            var data = this.roleApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.EnCode,
                    fullname = item.FullName
                };
                dictionary.Add(item.Id, fieldItem);
            }
            return dictionary;
        }
        private object GetDutyList()
        {
            var data = this.dutyApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.EnCode,
                    fullname = item.FullName
                };
                dictionary.Add(item.Id, fieldItem);
            }
            return dictionary;
        }
        private object GetMenuList()
        {
            if(!HttpContext.User.HasClaim(t => t.Type == OperatorModelClaimNames.RoleId))
            {
                return null;
            }
            string roleId = HttpContext.User.Claims.FirstOrDefault(t => t.Type== OperatorModelClaimNames.RoleId).Value;
            return ToMenuJson(this.roleAuthorizeApp.GetMenuList(roleId,HttpContext), "0");
        }
        private string ToMenuJson(List<ModuleEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<ModuleEntity> entitys = data.FindAll(t => t.ParentId == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.Id) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }
        private object GetMenuButtonList()
        {
            var roleId = User.Claims.First(t => t.Type == OperatorModelClaimNames.RoleId).Value;
            var data = this.roleAuthorizeApp.GetButtonList(roleId, HttpContext);
            var dataModuleId = data.Distinct(new ExtList<ModuleButtonEntity>("ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleButtonEntity item in dataModuleId)
            {
                var buttonList = data.Where(t => t.ModuleId.Equals(item.ModuleId));
                dictionary.Add(item.ModuleId, buttonList);
            }
            return dictionary;
        }
    }
}
