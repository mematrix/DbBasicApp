using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace DbBasicApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class CustomAuthAttribute : ActionFilterAttribute
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userName = context.HttpContext.Session.GetString("signin-user");
            if (string.IsNullOrEmpty(userName))
            {
                context.Result = new RedirectToActionResult(ActionName ?? "Login", ControllerName ?? "Account",
                    new Dictionary<string, object> { { "ReturnUrl", context.HttpContext.Request.Path.ToUriComponent() } });
            }
        }
    }
}