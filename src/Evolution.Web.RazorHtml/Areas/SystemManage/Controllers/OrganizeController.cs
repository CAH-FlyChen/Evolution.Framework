/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Framework;
using Evolution;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using Evolution.Web;
using Evolution.Data;
using Evolution.Data.Entity.SystemManage;
using Microsoft.AspNetCore.Authorization;

namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    [PermissionLevelDescrip("组织结构数据", "")]
    [Authorize]
    public class OrganizeController : EvolutionControllerBase
    {

    }
}
