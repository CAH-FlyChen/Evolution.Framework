/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Evolution.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using System.Threading.Tasks;
using Evolution.Web.Attributes;

namespace Evolution.Web.API.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class UserController : EvolutionControllerBase
    {
        #region 私有变量
        private UserService userApp = null;
        private UserLogOnService userLogOnApp = null;
        #endregion
        #region 构造函数
        public UserController(UserService userApp, UserLogOnService userLogOnApp)
        {
            this.userApp = userApp;
            this.userLogOnApp = userLogOnApp;
        }
        #endregion
        /// <summary>
        /// 获取用户表格数据
        /// </summary>
        /// <param name="pagination">分页对象</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await userApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await userApp.GetEntityById(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <param name="userLogOnEntity">用户登录实体</param>
        /// <param name="keyValue">Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            await userApp.Save(userEntity, userLogOnEntity, keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="keyValue">用户id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await userApp.Delete(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 重置密码页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RevisePassword()
        {
            return View();
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userPassword">用户密码</param>
        /// <param name="keyValue">用户id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitRevisePassword(string userPassword, string keyValue)
        {
            await userLogOnApp.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }
        /// <summary>
        /// 账号禁用
        /// </summary>
        /// <param name="keyValue">用户id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.Id = keyValue;
            userEntity.EnabledMark = false;
            await userApp.Update(userEntity);
            return Success("账户禁用成功。");
        }
        /// <summary>
        /// 启用账户
        /// </summary>
        /// <param name="keyValue">用户Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.Id = keyValue;
            userEntity.EnabledMark = true;
            await userApp.Update(userEntity);
            return Success("账户启用成功。");
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }
    }
}
