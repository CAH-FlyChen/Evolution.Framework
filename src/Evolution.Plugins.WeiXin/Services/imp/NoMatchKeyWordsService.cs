
using OvalTech.TechCloud.Module.WeiChat.IBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolution.Framework;
using Evolution.Plugins.WeiXin.Entities;
using Evolution.Domain.IRepository.SystemManage;

namespace Evolution.Plugins.WeiXin.Services
{
    public class NoMatchKeyWordsService : INoMatchKeyWordsService
    {
        private INoMatchKeywordsRepository repo = null;

        public NoMatchKeyWordsService(INoMatchKeywordsRepository repo)
        {
            this.repo = repo;
        }

        public Task<NoMatchKeywordsEntity> GetNoMatch(string appId,string tenantId)
        {
            return repo.FindEntityASync(t => t.TenantId == tenantId && t.AppId == appId);
        }

        public Task<int> UpdateNoMatch(NoMatchKeywordsEntity entity)
        {
            return repo.UpdateAsync(entity);
        }

        public Task<int> AddNoMatchKeyWords(NoMatchKeywordsEntity entity)
        {
            return repo.InsertAsync(entity);
        }


    }
}
