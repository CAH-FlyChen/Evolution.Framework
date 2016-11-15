using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.ScrumKanBan.Models
{
    public class UserStoryEntity: EntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Point { get; set; }
        /// <summary>
        /// 用户故事 新功能 bug 技术故事
        /// </summary>
        public int ItemTypeCode { get; set; }
        /// <summary>
        /// 未开始 已完成 。。。
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// 列表ID  current backlogice 等
        /// </summary>
        public string ListID { get; set; }
        public ProjectEntity Project { get; set; }
        public string AssignToUserId { get; set; }
    }
}
