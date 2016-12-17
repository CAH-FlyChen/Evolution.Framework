/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Evolution.Domain.Entity.SystemSecurity;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Evolution.Web.Areas.SystemSecurity.Controllers
{
    [Area("SystemSecurity")]
    [Authorize]
    public class DbBackupController : EvolutionControllerBase
    {
    }
}
