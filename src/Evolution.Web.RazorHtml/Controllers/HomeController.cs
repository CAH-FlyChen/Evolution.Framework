/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Evolution.Web;

namespace Evolution.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SDK()
        {
            return View();
        }
    }
}
