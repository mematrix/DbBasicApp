using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DbBasicApp.Filters;
using DbBasicApp.Models;
using DbBasicApp.Services;

namespace DbBasicApp.Controllers
{
    public class HomeController : Controller
    {
        [FromServices]
        public AccountService Service { get; set; }

        [FromServices]
        public AppDbContext DbContext { get; set; }

        [CustomAuth]
        public async Task<IActionResult> Index()
        {
            var user = await Service.GetCurrentUserAsync();
            return View(user);
        }

        [CustomAuth]
        public async Task<IActionResult> GetChildItem(string item)
        {
            var viewName = item;
            object model = null;
            try
            {
                var user = await Service.GetCurrentUserAsync();
                switch (item.ToLower())
                {
                    case "loginview": model = user; break;
                    case "userinfoview": model = user.UserInfo; break;
                    case "consumview": model = DbContext.PaymentRecords.Where(p => p.UserName == user.UserName); break;
                    case "payview":
                        model = DbContext.PaymentRecords.Where(p => p.UserName == user.UserName && p.PayOut > 0);
                        break;
                    case "deductview":
                        model = DbContext.PaymentRecords.Where(p => p.UserName == user.UserName && p.PayOut < 0);
                        break;
                    case "commentview":
                        model = DbContext.RatingRecords.Where(r => r.UserName == user.UserName)
                            .OrderByDescending(r => r.Time)
                            .GroupBy(r => r.SupporterName)
                            .Select(g => g.First());
                        break;
                    default: viewName = "Default"; break;
                }
                if (model == null)
                {
                    viewName = "Default";
                }
                return PartialView("~/Views/Partial/" + viewName + ".cshtml", model);
            }
            catch
            {
                return PartialView("~/Views/Partial/Default.cshtml");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Support()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public IActionResult Status(int id)
        {
            ViewData["Status"] = id;
            return View("~/Views/Shared/Status.cshtml");
        }
    }
}
