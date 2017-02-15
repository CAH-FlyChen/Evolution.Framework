using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Framework;

namespace Evolution.Application.SystemManage
{
    public interface IUserService
    {
        Task<UserEntity> CheckLogin(string username, string password, string tenantId);
        Task<int> Delete(string id,string tenantId);
        Task<UserEntity> GetEntityById(string id, string tenantId);
        Task<UserEntity> GetEntityByName(string userName, string tenantId);
        Task<List<UserEntity>> GetList(Pagination pagination, string keyword, string tenantId);
        Task<int> Save(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string id,string userId);
        Task<int> Update(UserEntity userEntity);
    }
}