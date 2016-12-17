using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Framework;

namespace Evolution.Application.SystemManage
{
    public interface IUserService
    {
        Task<UserEntity> CheckLogin(string username, string password);
        Task<int> Delete(string id);
        Task<UserEntity> GetEntityById(string id);
        Task<UserEntity> GetEntityByName(string userName);
        Task<List<UserEntity>> GetList(Pagination pagination, string keyword);
        Task<int> Save(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string id);
        Task<int> Update(UserEntity userEntity);
    }
}