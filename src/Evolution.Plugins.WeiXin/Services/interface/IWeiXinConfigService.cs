using Evolution.Plugins.WeiXin.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.WeiXin.IServices
{
    public interface IWeiXinConfigService
    {
        /// <summary>
        /// 获取所有微信公众号配置信息
        /// </summary>
        /// <returns></returns>
        Task<List<WeiXinConfigEntity>> GetList();
        /// <summary>
        /// 通过id获取微信公众号的配置
        /// </summary>
        /// <param name="Id">微信公众号数据库Id</param>
        /// <returns></returns>
        Task<WeiXinConfigEntity> GetForm(string Id);
        Task<int> Delete(string Id);
        Task<int> Save(WeiXinConfigEntity configEntity, string keyValue, string userId);
    }
}
