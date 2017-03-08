using Evolution.Framework;
using Evolution.Plugins.WeiXin.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evolution.Plugins.WeiXin.IServices
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public interface IWeiXinUserService
    {
        /// <summary>
        /// 获取所有的微信用户列表
        /// </summary>
        /// <returns></returns>
        Task<List<WeiXinUserEntity>> GetAllByTenantId(string tenantId,Pagination p);
        /// <summary>
        /// 通过微信AppId获取全部微信用户
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        IAsyncEnumerable<WeiXinUserEntity> GetAllByAppId(string appId,string tenantId, Pagination p);
        /// <summary>
        /// 获取微信用户信息
        /// </summary>
        /// <param name="wxUserId">微信用户Id</param>
        /// <returns></returns>
        Task<WeiXinUserEntity> GetWXUserInfo(string wxUserId);
    }
}
