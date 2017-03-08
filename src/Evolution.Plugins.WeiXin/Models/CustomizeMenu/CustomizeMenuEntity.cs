using Evolution.Data;
using Evolution.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Evolution.Plugins.WeiXin;

namespace Evolution.Plugins.WeiXin.Entities
{
    public enum MenuActionType
    {
        链接 = 0,
        图文 = 1,
        多图文 = 2
    };
    /// <summary>
    /// 存储菜单名称及结构
    /// </summary>
    public class CustomizeMenuEntity: EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string AppId { get; set; }
        public string Title { get; set; }
        public decimal LevelNUM { get; set; }
        public string ParentId { get; set; }
        /// <summary>
        /// 动作类型
        /// </summary>
        public MenuActionType ActionType { get; set; }
        /// <summary>
        /// 动作Id
        /// </summary>
        public string ActionId { get; set; }
        public int NeedOAuth { get; set; }
        public string TenantId { get; set; }
    }
}
