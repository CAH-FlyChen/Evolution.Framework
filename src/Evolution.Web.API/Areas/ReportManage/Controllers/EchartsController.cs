/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Evolution.Web.API.Areas.ReportManage.Controllers
{
    //ReportManage
    [Area("ReportManage")]
    public class EchartsController : Controller
    {
        //
        // GET: /ReportManage/Echarts/

        public ActionResult Index()
        {
            return View();
        }

    }
}
