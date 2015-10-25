using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using DbBasicApp.Services;
using Microsoft.AspNet.Mvc.Filters;

namespace DbBasicApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthenticAttribute : CustomAuthAttribute
    {
        public int Level { get; set; } = 2;
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if(context.Result==null)
            {
                var service = context.HttpContext.ApplicationServices.GetRequiredService<AccountService>();
                var user = service.GetCurrentUserAsync().Result;
                if (user == null || user.Level < Level)
                {
                    var controller = context.Controller as Controller;
                    if (controller == null)
                    {
                        context.Result = new HttpStatusCodeResult(403);
                    }
                    else
                    {
                        context.Result = controller.View("~/Views/Shared/Forbidden.cshtml");
                    }
                }
            }
        }
    }
}