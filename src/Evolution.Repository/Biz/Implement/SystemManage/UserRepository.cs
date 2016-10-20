/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Evolution.Data;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using Microsoft.AspNetCore.Http;
using Evolution.IInfrastructure;
using Evolution.Repository;
using System.Threading.Tasks;

namespace Evolution.Repository.SystemManage
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        #region 私有变量
        private HttpContext context = null;
        #endregion
        #region 构造函数
        public UserRepository(EvolutionDBContext ctx, IHttpContextAccessor contextAccessor) : base(ctx)
        {
            context = contextAccessor.HttpContext;
        }
        #endregion
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户Id</param>
        public Task<int> Delete(string id)
        {
            using (var db = new RepositoryBase(dbcontext).BeginTrans())
            {
                db.DeleteAsync<UserEntity>(t => t.Id == id);
                db.DeleteAsync<UserLogOnEntity>(t => t.UserId == id);
                return db.CommitAsync();
            }
        }
/// <summary>
        /// 保存用户。若id空则创建用户实体和登录实体，否则只更新用户实体
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <param name="userLogOnEntity">用户登录实体</param>
        /// <param name="id">Id</param>
        public Task<int> Save(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string id)
        {
            using (var repo = new RepositoryBase(dbcontext).BeginTrans())
            {
                if (!string.IsNullOrEmpty(id))
                {
                    repo.UpdateAsync(userEntity);
                }
                else
                {
                    userEntity.AttachCreateInfo(context);
                    userLogOnEntity.Id = userEntity.Id;
                    userLogOnEntity.UserId = userEntity.Id;
                    userLogOnEntity.UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                    userLogOnEntity.UserPassword = Encryptor.EncryptPWD(userLogOnEntity.UserPassword.ToLower(), userLogOnEntity.UserSecretkey);
                    repo.InsertAsync(userEntity);
                    repo.InsertAsync(userLogOnEntity);
                    
                }
                return repo.CommitAsync();
            }
        }
    }
}
