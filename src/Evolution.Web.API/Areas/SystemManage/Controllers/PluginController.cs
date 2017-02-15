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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Evolution.Web.API.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class PluginController : EvolutionControllerBase
    {
        #region 私有变量
        private PluginService service = null;
        #endregion
        #region 构造函数
        public PluginController(PluginService service)
        {
            this.service = service;
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
                rows = await service.GetList(pagination, keyword,this.TenantId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult Activate(string pluginId)
        {
            var r = service.Activate(pluginId);
            return Content(r.ToJson());
        }
    }
}
