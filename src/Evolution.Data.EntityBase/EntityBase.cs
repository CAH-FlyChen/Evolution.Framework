using Evolution.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Framework;

namespace Evolution.Data
{
    public class EntityBase : ICreationAudited, IModificationAudited, IDeleteAudited
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int? SortCode { get; set; }
        public bool? DeleteMark { get; set; }
        public bool? EnabledMark { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifyUserId { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteUserId { get; set; }

        public void AttachCreateInfo(HttpContext httpContext)
        {
            var entity = this as ICreationAudited;
            this.Id = Common.GuId();
            //get userid
            var userClaim = httpContext.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.UserId);
            if (userClaim != null)
                entity.CreatorUserId = userClaim.Value;
            entity.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 修改实体的Modify属性。涉及：修改者和修改日期
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <param name="httpContext"></param>
        public void AttachModifyInfo(string id, HttpContext httpContext)
        {
            var entity = this as IModificationAudited;
            this.Id = id;
            var userId = httpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserId).Value;
            entity.LastModifyUserId = userId;
            entity.LastModifyTime = DateTime.Now;
        }
        public void AttachRemoveInfo(HttpContext httpContext)
        {
            var entity = this as IDeleteAudited;
            var userId = httpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserId).Value;
            entity.DeleteUserId = userId;
            entity.DeleteTime = DateTime.Now;
            entity.DeleteMark = true;
        }
    }
}
