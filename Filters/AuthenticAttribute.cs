using System;
using DbBasicApp.Services;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;

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
                    //context.Result= context.Controller
                }
            }
        }
    }
}