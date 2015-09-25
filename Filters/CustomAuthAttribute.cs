using Microsoft.AspNet.Mvc;

namespace DbBasicApp.Filters
{
    public class CustomAuthAttribute : ActionFilterAttribute
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult(ActionName ?? "Login", ControllerName ?? "Account", null);
            }
        }
    }
}