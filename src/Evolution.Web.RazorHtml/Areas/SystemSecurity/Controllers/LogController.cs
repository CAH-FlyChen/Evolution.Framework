/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Evolution.Web.Areas.SystemSecurity.Controllers
{
    [Area("SystemSecurity")]
    [Authorize]
    public class LogController : EvolutionControllerBase
    {
    }
}
