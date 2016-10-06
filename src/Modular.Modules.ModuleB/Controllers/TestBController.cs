using Evolution.Data;
using Microsoft.AspNetCore.Mvc;
using Modular.Modules.ModuleB.Module;
using Modular.Modules.ModuleB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleB.Controllers
{
    public class TestBController : Controller
    {
        public IDemoService service = null;
        public TestBController(IDemoService s)
        {
            this.service = s;
        }

        public IActionResult Index()
        {
            this.service.GetStr();   
            return View();
        }
    }
}
