using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace Evolution.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HandlerAjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public bool Ignore { get; set; }
        public HandlerAjaxOnlyAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            if (Ignore)
                return true;
            return routeContext.HttpContext.Request.IsAjaxRequest();
        }
    }

    public static class AjaxRequestExtensions {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            bool r = request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if(r)
            {
                return true;
            }
            else
            {
                throw (new InvalidOperationException("此Url请求必须为X-Requested-With：XMLHttpRequest"));
            }

        }
    }
}
