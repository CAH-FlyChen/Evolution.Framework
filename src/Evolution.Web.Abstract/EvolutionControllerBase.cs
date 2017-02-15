
using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Evolution.Web
{
    //[HandlerLogin]
    public abstract class EvolutionControllerBase : Controller
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Form()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Details()
        {
            return View();
        }
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }

        public string TenantId
        {
            get {
                return HttpContext.Request.Headers["TenantId"];
            }
        }
        public string UserId
        {
            get
            {
                return HttpContext.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.UserId).Value;
            }
        }
        public string IsSystem
        {
            get
            {
                return HttpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.IsSystem).Value;
            }
        }
    }
}
