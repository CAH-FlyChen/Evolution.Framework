//using Evolution.Framework;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using NFine.Application.SystemManage;
//using System.Linq;
//using System.Text;

//namespace NFine.Web
//{
//    public class HandlerAuthorizeAttribute : ActionFilterAttribute
//    {
//        RoleAuthorizeApp roleAuthorizeApp = null;
//        public bool Ignore { get; set; }
//        public HandlerAuthorizeAttribute(RoleAuthorizeApp roleAuthorizeApp,bool ignore = true)
//        {
//            Ignore = ignore;
//            this.roleAuthorizeApp = roleAuthorizeApp;
//        }
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            //var isSystem = httpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.IsSystem).Value;
//            //if (isSystem.ToBool())
//            //{
//            //    return;
//            //}
//            //if (Ignore == false)
//            //{
//            //    return;
//            //}
//            //if (!this.ActionAuthorize(filterContext))
//            //{
//            //    StringBuilder sbScript = new StringBuilder();
//            //    sbScript.Append("<script type='text/javascript'>alert('很抱歉！您的权限不足，访问被拒绝！');</script>");
//            //    filterContext.Result = new ContentResult() { Content = sbScript.ToString() };
//            //    return;
//            //}
//        }
//        private bool ActionAuthorize(ActionExecutingContext filterContext)
//        {
//            var roleId = filterContext.HttpContext.User.Claims.First(t => t.Type == OperatorModelClaimNames.RoleId).Value;
//            //var moduleId = WebHelper.GetCookie("nfine_currentmoduleid", filterContext.HttpContext);
            

//           var action = "";// filterContext.HttpContext.Request.ServerVariables["SCRIPT_NAME"].ToString();

//            return roleAuthorizeApp.ActionValidate(roleId, moduleId, action);
//        }
//    }
//}