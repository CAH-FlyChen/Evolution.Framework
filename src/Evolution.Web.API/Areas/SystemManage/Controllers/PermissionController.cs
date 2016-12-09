//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Evolution.Web;
//using Evolution.Framework;
//using Evolution.Application.SystemManage;
//using Evolution.Domain.Entity.SystemManage;
//using Evolution.Web.Controllers;
//using System.Reflection;

//// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

//namespace Evolution.Web.API.Areas.SystemManage.Controllers
//{
//    [Area("SystemManage")]
//    public class PermissionController : Controller
//    {
//        PermissionApp service = null;

//        public PermissionController(PermissionApp service)
//        {
//            this.service = service;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        [HandlerAjaxOnly]
//        public ActionResult GetGridJson(Pagination pagination, string keyword)
//        {
//            var list = service.GetList(this.GetType(),keyword);

//            var data = new
//            {
//                rows = list,
//                total = list.Count(),
//                page = 1,
//                records = list.Count()
//            };
//            return Content(data.ToJson());
//        }
//    }
//}
