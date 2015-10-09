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
            return PartialView("~/Views/Partial/SearchResultView.cshtml", model);
        }

        public async Task<IActionResult> UserInfo(string id)
        {
            var user = await Service.GetCurrentUserAsync();
            var model = await DbContext.LoginInfos.Include(l => l.UserInfo)
                .FirstOrDefaultAsync(l => l.UserName == id);
            // 在用户已经登录的情况下，如果访问了一个不存在的用户的信息，
            // 又或者访问的id为自己，那么将跳转到用户自己主页面。
            if (user != null && (model == null || model.UserName == user.UserName))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            
            ViewData["IsLogin"] = user != null;
            return View(model);
        }

        [CustomAuth]
        public async Task<IActionResult> Comment(string id)
        {
            var user = await Service.GetCurrentUserAsync();
            if (user.UserName == id)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            
            var model = await DbContext.LoginInfos.FirstOrDefaultAsync(l => l.UserName == id);
            return View(model);
        }

        [CustomAuth]
        public async Task<IActionResult> Chat(string id)
        {
            var user = await Service.GetCurrentUserAsync();
            if (user.UserName == id)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            
            var model = await DbContext.LoginInfos.FirstOrDefaultAsync(l => l.UserName == id);
            return View(model);
        }
    }
}