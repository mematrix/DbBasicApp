using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using DbBasicApp.Models;
using DbBasicApp.Services;
using DbBasicApp.Util;

namespace DbBasicApp.Controllers
{
    public class HomeController : Controller
    {
        [FromServices]
        public AccountService Service { get; set; }

        [FromServices]
        public AppDbContext DbContext { get; set; }

        public async Task<IActionResult> Index()
        {
            DbHelper.EnsureDatabaseCreated(DbContext);
            var model = await DbContext.TelePackages.ToListAsync();
            var user = await Service.GetCurrentUserAsync();
            ViewData["IsCashier"] = user != null && user.Level == 2;
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Super()
        {
            DbHelper.EnsureDatabaseCreated(DbContext);
            var model = await DbContext.LoginInfos.Include(l => l.UserInfo).ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> SetLevel(string userName, int level)
        {
            if (level > 2 || level < 0) return Json(new { status = false, msg = "错误的身份设置！" });

            var user = await DbContext.LoginInfos.FirstOrDefaultAsync(l => l.UserName == userName);
            if (user == null) return Json(new { status = false, msg = "用户不存在！" });

            var update = user.Level != level;
            user.Level = level;
            DbContext.LoginInfos.Update(user);
            await DbContext.SaveChangesAsync();
            return Json(new { status = true, msg = "操作成功", update = update, nlevel = level });
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
