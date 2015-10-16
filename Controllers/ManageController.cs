using System.Threading.Tasks;
using DbBasicApp.Services;
using Microsoft.AspNet.Mvc;

namespace DbBasicApp.Controllers
{
    public class ManageController : Controller
    {
        [FromServices]
        public AccountService Service { get; set; }

        public async Task<IActionResult> Charge()
        {
            var user = await Service.GetCurrentUserAsync();
            if (user == null || user.Level != 2)
            {
                return View("~/Views/Shared/Forbidden.cshtml");
            }
            return View();
        }
    }
}