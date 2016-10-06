using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.EvolutionPlugin.Controllers
{
    public class PluginDemo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            return Content("Now is : " + DateTime.Now.ToString());
        }

    }
}
