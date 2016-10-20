﻿using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Evolution.Web.Attributes
{
    public class HandlerErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new ContentResult { Content = new AjaxResult { state = ResultType.error.ToString(), message = context.Exception.Message }.ToJson() };
        }
        private void WriteLog(ExceptionContext context)
        {
            if (context == null)
                return;
            //var log = LogFactory.GetLogger(context.Controller.ToString());
            //log.Error(context.Exception);
        }
    }
}