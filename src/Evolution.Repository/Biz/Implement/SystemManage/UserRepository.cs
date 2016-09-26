/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Framework;
using Evolution.Data;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Repository.SystemManage
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        HttpContext context = null;
        public UserRepository(EvolutionDbContext ctx, IHttpContextAccessor contextAccessor) : base(ctx)
        {
            context = contextAccessor.HttpContext;
        }
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase(dbcontext).BeginTrans())
            {
                db.Delete<UserEntity>(t => t.Id == keyValue);
                db.Delete<UserLogOnEntity>(t => t.UserId == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            using (var repo = new RepositoryBase(dbcontext).BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    repo.Update(userEntity);
                }
                else
                {
                    userEntity.Create(context);
                    userLogOnEntity.Id = userEntity.Id;
                    userLogOnEntity.UserId = userEntity.Id;
                    userLogOnEntity.UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                    userLogOnEntity.UserPassword = Md5.md5(
                        AESEncrypt.Encrypt(
                            userLogOnEntity.UserPassword.ToLower(), 
                            userLogOnEntity.UserSecretkey
                            ).ToLower(), 
                            32
                        ).ToLower();
                    repo.Insert(userEntity);
                    repo.Insert(userLogOnEntity);
                }
                repo.Commit();
            }
        }
    }
}
