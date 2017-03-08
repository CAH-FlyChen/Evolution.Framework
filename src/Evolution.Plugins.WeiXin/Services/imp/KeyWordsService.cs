
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
    public class KeyWordsService : IKeyWordsService
    {
        private IKeywordsRepository repo = null;

        public KeyWordsService(IKeywordsRepository repo)
        {
            this.repo = repo;
        }

        public Task<List<KeywordsEntity>> GetKeyWordsList(string appid, string tenantId, Pagination p)
        {
            return 
            repo.FindListAsync(t => t.TenantId == tenantId && t.AppId == appid, p);
        }


        public Task<KeywordsEntity> GetKeyWords(string id, string appId,string tenantId)
        {
            return repo.FindEntityASync(t => t.Id == id && t.AppId == appId && t.TenantId == tenantId);
        }

        public Task<int> AddKeyWords(KeywordsEntity entity)
        {
            return repo.InsertAsync(entity);
        }

        public Task<int> DeleteKeyWords(string id,string currentUserId,string appid,string tenantId)
        {
            KeywordsEntity entity = repo.FindEntityASync(t => t.Id == id && t.AppId == appid && t.TenantId == tenantId).Result;
            entity.DeleteMark = true;
            entity.DeleteTime = DateTime.Now;
            entity.DeleteUserId = currentUserId;
            return repo.UpdateAsync(entity);
        }

        public Task<int> UpdateKeyWords(KeywordsEntity entity)
        {
            return repo.UpdateAsync(entity);
        }

        public Task<KeywordsEntity> GetKeyWordsByName(string keywords, string appId,string tenantId)
        {
            return repo.FindEntityASync(t => t.KeywordsText == keywords && t.AppId == appId && t.TenantId == tenantId);
        }


    }
}
