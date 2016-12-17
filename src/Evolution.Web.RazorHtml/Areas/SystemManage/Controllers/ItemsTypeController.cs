/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    [Authorize]
    public class ItemsTypeController : EvolutionControllerBase
    {
    }
}
