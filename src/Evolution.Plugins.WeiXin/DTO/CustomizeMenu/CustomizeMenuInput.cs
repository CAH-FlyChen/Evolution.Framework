using Evolution.Plugins.WeiXin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.WeiXin.DTO.CustomizeMenu
{
    public class CustomizeMenuInput
    {
        public string Id { get; set; }
        public string AppId { get; set; }

        public string Title { get; set; }

        public string Level { get; set; }

        public string ParentId { get; set; }
        public string ActionId { get; set; }
        public MenuActionType ActionType { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图文消息Id
        /// </summary>
        public string NewsId { get; set; }
        public int SortCode { get; set; }
        public int NeedOAUTH { get; set; }
        public string UserId { get; set; }
        public string TenantId { get; set; }
    }
}
