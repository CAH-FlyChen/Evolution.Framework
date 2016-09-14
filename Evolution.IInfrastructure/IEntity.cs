///*******************************************************************************
// * Copyright © 2016 NFine.Framework 版权所有
// * Author: NFine
// * Description: NFine快速开发平台
// * Website：http://www.nfine.cn
//*********************************************************************************/
//using Evolution.Framework;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Linq;

//namespace Evolution.Domain
//{
//    public class IEntity
//    {
//        public void Create(HttpContext httpContext)
//        {
//            var entity = this as ICreationAudited;
//            entityId = Common.GuId();
//            //get userid
//            var userClaim = httpContext.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.UserId);
//            if(userClaim!=null)
//            entityCreatorUserId = userClaim.Value;
//            entityCreatorTime = DateTime.Now;
//        }
//        public void Modify(string keyValue, HttpContext httpContext)
//        {
//            var entity = this as IModificationAudited;
//            entityId = keyValue;
//            var userId = httpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserId).Value;
//            entityLastModifyUserId = userId;
//            entityLastModifyTime = DateTime.Now;
//        }
//        public void Remove(HttpContext httpContext)
//        {
//            var entity = this as IDeleteAudited;
//            var userId = httpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserId).Value;
//            entityDeleteUserId = userId;
//            entityDeleteTime = DateTime.Now;
//            entityDeleteMark = true;
//        }
//    }
//}
