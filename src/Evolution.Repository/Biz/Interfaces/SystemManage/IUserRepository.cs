/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Domain.Entity.SystemManage;
using Evolution.IRepository;

namespace Evolution.Domain.IRepository.SystemManage
{
    public interface IUserRepository : IRepositoryBase<UserEntity>
    {
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户Id</param>
        void Delete(string id);
        /// <summary>
        /// 保存用户。
        /// 若id空则创建用户实体和登录实体，否则只更新用户实体
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <param name="userLogOnEntity">用户登录实体</param>
        /// <param name="id">Id</param>
        void Save(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string id);
    }
}
