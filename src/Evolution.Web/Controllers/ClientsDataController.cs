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
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using Evolution.Data.Entity.SystemManage;

namespace Evolution.Web.Controllers
{
    //[HandlerLogin]
    public class ClientsDataController : Controller
    {
        #region 私有变量
        private ItemsDetailService itemDetailApp = null;
        private ItemsService itemsApp = null;
        private OrganizeService organizeApp = null;
        private RoleService roleApp = null;
        private DutyService dutyApp = null;
        private RoleAuthorizeService roleAuthorizeApp = null;
        private MenuService menuApp = null;
        private MenuButtonService menuButtonApp = null;
        #endregion
        #region 构造函数
        public ClientsDataController(DutyService dutyApp, OrganizeService organizeApp, ItemsDetailService itemDetailApp, ItemsService itemsApp, RoleService roleApp, RoleAuthorizeService roleAuthorizeApp, MenuService menuApp, MenuButtonService menuButtonApp)
        {
            this.itemDetailApp = itemDetailApp;
            this.itemsApp = itemsApp;
            this.organizeApp = organizeApp;
            this.roleApp = roleApp;
            this.dutyApp = dutyApp;
            this.roleAuthorizeApp = roleAuthorizeApp;
            this.menuApp = menuApp;
            this.menuButtonApp = menuButtonApp;
        }
        #endregion 
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetClientsDataJson()
        {
            var data = new
            {
                dataItems = await this.GetDataItemList(),
                organize = await this.GetOrganizeList(),
                role = await this.GetRoleList(),
                duty = await this.GetDutyList(),
                user = "",
                authorizeMenu = await this.GetMenuList(),
                authorizeButton = await this.GetMenuButtonList(),
            };
            return Content(data.ToJson());
        }
        private async Task<object> GetDataItemList()
        {
            var itemdata = await itemDetailApp.GetList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in await itemsApp.GetList())
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
        private async Task<object> GetOrganizeList()
        {
            var data = await this.organizeApp.GetList();
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
        private async Task<object> GetRoleList()
        {
            var data = await this.roleApp.GetList();
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
            return Task.FromResult<object>(dictionary);
        }
        private async Task<object> GetDutyList()
        {
            var data = await this.dutyApp.GetList();
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
            return Task.FromResult<object>(dictionary);
        }
        private async Task<object> GetMenuList()
        {
            if(!HttpContext.User.HasClaim(t => t.Type == OperatorModelClaimNames.RoleId))
            {
                return null;
            }
            string roleId = HttpContext.User.Claims.FirstOrDefault(t => t.Type== OperatorModelClaimNames.RoleId).Value;
            return ToMenuJson(await this.menuApp.GetMenuListByRoleId(roleId), "0");
        }
        private string ToMenuJson(List<MenuEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<MenuEntity> entitys = data.FindAll(t => t.ParentId == parentId);
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
        private async Task<object> GetMenuButtonList()
        {
            var roleId = User.Claims.First(t => t.Type == OperatorModelClaimNames.RoleId).Value;
            var authedButtonList = await menuButtonApp.GetButtonListByRoleId(roleId);
            var distinctAuthedMenuButtonList = authedButtonList.Distinct(new ExtList<MenuButtonEntity>("MenuId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (MenuButtonEntity item in distinctAuthedMenuButtonList)
            {
                var buttonList = authedButtonList.Where(t => t.MenuId.Equals(item.MenuId));
                dictionary.Add(item.MenuId, buttonList);
            }
            return dictionary;
        }
    }
}
