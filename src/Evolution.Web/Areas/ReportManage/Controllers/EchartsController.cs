﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NFine.Web.Areas.ReportManage.Controllers
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
