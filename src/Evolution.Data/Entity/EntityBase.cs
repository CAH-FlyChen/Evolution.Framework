using Evolution.Domain;
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Data.Entity
{
    public class EntityBase
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int? SortCode { get; set; }
        public bool? DeleteMark { get; set; }
        public bool? EnabledMark { get; set; }
        public DateTime? CreatorTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifyUserId { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteUserId { get; set; }

        public void Create(HttpContext httpContext)
        {
            var entity = this as ICreationAudited;
            this.Id = Common.GuId();
            //get userid
            var userClaim = httpContext.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.UserId);
            if (userClaim != null)
                entity.CreatorUserId = userClaim.Value;
            entity.CreatorTime = DateTime.Now;
        }
        public void Modify(string keyValue, HttpContext httpContext)
        {
            var entity = this as IModificationAudited;
            this.Id = keyValue;
            var userId = httpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserId).Value;
            entity.LastModifyUserId = userId;
            entity.LastModifyTime = DateTime.Now;
        }
        public void Remove(HttpContext httpContext)
        {
            var entity = this as IDeleteAudited;
            var userId = httpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserId).Value;
            entity.DeleteUserId = userId;
            entity.DeleteTime = DateTime.Now;
            entity.DeleteMark = true;
        }
    }
}
