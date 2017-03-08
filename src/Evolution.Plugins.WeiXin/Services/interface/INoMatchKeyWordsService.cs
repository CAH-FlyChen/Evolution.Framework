using Evolution.Framework;
using Evolution.Plugins.WeiXin.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OvalTech.TechCloud.Module.WeiChat.IBLL
{
    public interface INoMatchKeyWordsService
    {
        Task<NoMatchKeywordsEntity> GetNoMatch(string appId, string tenantId);
        Task<int> UpdateNoMatch(NoMatchKeywordsEntity entity);
        Task<int> AddNoMatchKeyWords(NoMatchKeywordsEntity entity);
    }
}
