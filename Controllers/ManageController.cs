using Microsoft.AspNet.Mvc;
using DbBasicApp.Filters;
using DbBasicApp.Services;

namespace DbBasicApp.Controllers
{
    public class ManageController : Controller
    {
        [FromServices]
        public AccountService Service { get; set; }

        [Authentic]
        public IActionResult Charge()
        {
            return View();
        }
    }
}