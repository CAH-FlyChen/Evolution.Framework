﻿/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Web.Areas.SystemSecurity.Controllers
{
    [Area("SystemSecurity")]
    [Authorize]
    public class ServerMonitoringController : EvolutionControllerBase
    {
       
    }
}
