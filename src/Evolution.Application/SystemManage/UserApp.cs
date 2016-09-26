/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Evolution.Data;
using Evolution.IInfrastructure;

namespace Evolution.Application.SystemManage
{
    public class UserApp 
    {
        #region 私有变量

        private IUserRepository service = null;
        private UserLogOnApp userLogOnApp = null;
        private RoleApp roleApp = null;
        private HttpContext currentContext = null;

        #endregion

        #region 构造函数
        public UserApp (IUserRepository service, UserLogOnApp userLogOnApp, RoleApp roleApp,IHttpContextAccessor accessor)
        {
            this.service = service;
            this.userLogOnApp = userLogOnApp;
            this.currentContext = accessor.HttpContext;
            this.roleApp = roleApp;
        }
        #endregion

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pagination">分页信息</param>
        /// <param name="keyword">关键字，只允用户名，真实姓名，电话号码</param>
        /// <returns>用户实体列表</returns>
        public List<UserEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UserEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Account.Contains(keyword));
                expression = expression.Or(t => t.RealName.Contains(keyword));
                expression = expression.Or(t => t.MobilePhone.Contains(keyword));
            }
            expression = expression.And(t => t.Account != "admin");
            return service.FindList(expression, pagination);
        }
        /// <summary>
        /// 通过id获取用户实体对象
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns>用户实体对象</returns>
        public UserEntity GetEntityById(string id)
        {
            return service.FindEntity(id);
        }
        /// <summary>
        /// 删除用户实体对象
        /// </summary>
        /// <param name="id">用户id</param>
        public void Delete(string id)
        {
            service.Delete(id);
        }
        /// <summary>
        /// 保存用户对象
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <param name="userLogOnEntity">登录实体</param>
        /// <param name="id">用户Id，为空则创建实体，否则更新实体</param>
        public void Save(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string id)
        {
            if (!string.IsNullOrEmpty(id))
                userEntity.AttachModifyInfo(id, currentContext);
            else
                userEntity.AttachCreateInfo(currentContext);
            service.Save(userEntity, userLogOnEntity, id);
        }
        /// <summary>
        /// 更新用户实体
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        public void Update(UserEntity userEntity)
        {
            service.Update(userEntity);
        }

        /// <summary>
        /// 验证用户名密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">md5（16位）加密后的秘密</param>
        /// <returns></returns>
        public UserEntity CheckLogin(string username, string password)
        {
            //获取用户对象
            UserEntity userEntity = service.FindEntity(t => t.Account == username);
            if (userEntity == null) throw new Exception("账户不存在，请重新输入");
            if (userEntity.EnabledMark == false) throw new Exception("账户被系统锁定,请联系管理员");
            //获取用户登录对象
            UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.Id);
            //密码0000 MD5加密后 de54ef2d07c608096fddb77a27c5f126
            //验证密码
            string pwd = Tools.CaculatePWD(password, userLogOnEntity.UserSecretkey);
            if (pwd == userLogOnEntity.UserPassword) throw new Exception("密码不正确，请重新输入");
            //记录登录日志
            WriteLoginLog(userLogOnEntity);
            return userEntity;
        }

        #region 私有方法
        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="userLogOnEntity">用户登录对象</param>
        private void WriteLoginLog(UserLogOnEntity userLogOnEntity)
        {
            DateTime lastVisitTime = DateTime.Now;
            int LogOnCount = (userLogOnEntity.LogOnCount).ToInt() + 1;
            if (userLogOnEntity.LastVisitTime != null)
                userLogOnEntity.PreviousVisitTime = userLogOnEntity.LastVisitTime.ToDate();
            userLogOnEntity.LastVisitTime = lastVisitTime;
            userLogOnEntity.LogOnCount = LogOnCount;
            userLogOnApp.UpdateForm(userLogOnEntity);
        }
        #endregion
    }
}
