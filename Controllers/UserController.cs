using System.Linq;
using System.Threading.Tasks;
using DbBasicApp.Filters;
using DbBasicApp.Models;
using DbBasicApp.Services;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace DbBasicApp.Controllers
{
    public class UserController : Controller
    {
        [FromServices]
        public AccountService Service { get; set; }

        [FromServices]
        public AppDbContext DbContext { get; set; }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(UserController.Discover));
        }

        public async Task<IActionResult> Discover()
        {
            var model = await DbContext.LoginInfos.Where(l => l.Level == 1 || l.Level == 2).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> SearchUser(string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return null;
            var model = await DbContext.LoginInfos.Include(l => l.UserInfo)
                .Where(l => l.UserName.Contains(q))
                .ToListAsync();
            return PartialView("~/Views/Partial/SearchResult.cshtml", model);
        }

        public async Task<IActionResult> UserInfo(string id)
        {
            var user = await Service.GetCurrentUserAsync();
            var model = await DbContext.LoginInfos.Include(l => l.UserInfo)
                .FirstOrDefaultAsync(l => l.UserName == id);
            if (user != null && model == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["IsLogin"] = user != null;
            return View(model);
        }

        [CustomAuth]
        public async Task<IActionResult> Comment(string id)
        {
            var user = await DbContext.LoginInfos.FirstOrDefaultAsync(l => l.UserName == id);
            return View(user);
        }

        [CustomAuth]
        public async Task<IActionResult> Chat(string id)
        {
            var user = await DbContext.LoginInfos.FirstOrDefaultAsync(l => l.UserName == id);
            return View(user);
        }
    }
}