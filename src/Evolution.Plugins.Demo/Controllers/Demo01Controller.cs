using Evolution.Data;
using Evolution.Plugins.Demo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.Demo.Controllers
{
    [Area("Demos")]
    public class Demo01Controller : Controller
    {
        public IDemoService service = null;
        public Demo01Controller(IDemoService s)
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
