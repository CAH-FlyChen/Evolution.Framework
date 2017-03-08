using Evolution.Framework;
using Evolution.Plugins.WeiXin.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OvalTech.TechCloud.Module.WeiChat.IBLL
{
    public interface IKeyWordsService
    {
        Task<List<KeywordsEntity>> GetKeyWordsList(string appid,string tenantId, Pagination p);
        Task<int> AddKeyWords(KeywordsEntity entity);
        Task<KeywordsEntity> GetKeyWords(string id, string appId, string tenantId);
        Task<int> DeleteKeyWords(string id, string currentUserId, string appid, string tenantId);
        Task<int> UpdateKeyWords(KeywordsEntity entity);
        Task<KeywordsEntity> GetKeyWordsByName(string keywords, string appId, string tenantId);

        //int AddNoMatchKeyWords(string mpid, string userid, string resType, string newsId, string content);
        //int DeleteKeyWords(string id, string status);
        //System.Data.DataRow GetKeyWords(string mpid, string id);
        //System.Data.DataRow GetKeyWordsByName(string keywords, string mpid);

        //System.Data.DataRow GetNoMatch(string mpid);
        //int UpdateKeyWords(string id, string keywords, string userid, string matchType, string resType, string newsId, string content);
        //void updatenomatch(string mpid, string userid, string resType, string newsId, string content);
    }
}
