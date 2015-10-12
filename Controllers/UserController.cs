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
            if (string.IsNullOrWhiteSpace(q)) return new EmptyResult();
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

            RatingRecord model = null;
            var supUser = await DbContext.LoginInfos.Include(l => l.UserInfo).FirstOrDefaultAsync(l => l.UserName == id);
            if (supUser != null)
            {
                model = await DbContext.RatingRecords.FirstOrDefaultAsync(r =>
                    r.UserName == user.UserName && r.SupporterName == supUser.UserName);
                if (model == null)
                {
                    model = new RatingRecord
                    {
                        UserName = user.UserName,
                        SupporterName = supUser.UserName,
                        Rating = 0,
                        RatingMsg = "",
                        Time = System.DateTime.Now,
                        SupporterInfo = supUser
                    };
                }
                else
                {
                    model.SupporterInfo = supUser;
                }
            }
            ViewData["RatingValid"] = "";
            return View(model);
        }

        [HttpPost]
        [CustomAuth]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(string userName, int rating, string comment)
        {
            var user = await Service.GetCurrentUserAsync();
            if (user.UserName == userName)
            {
                return new BadRequestResult();
            }

            RatingRecord model = null;
            var supUser = await DbContext.LoginInfos.Include(l => l.UserInfo)
                .FirstOrDefaultAsync(l => l.UserName == userName);
            if (supUser != null)
            {
                if (rating > 5 || rating < 1)
                {
                    ViewData["RatingValid"] = "评分不可为空！";
                    return View(new RatingRecord
                    {
                        UserName = user.UserName,
                        SupporterName = supUser.UserName,
                        Rating = 0,
                        RatingMsg = comment,
                        Time = System.DateTime.Now,
                        SupporterInfo = supUser
                    });
                }

                ViewData["RatingValid"] = "";
                model = await DbContext.RatingRecords.FirstOrDefaultAsync(r =>
                    r.UserName == user.UserName && r.SupporterName == supUser.UserName);
                if (model == null)
                {
                    model = new RatingRecord
                    {
                        UserName = user.UserName,
                        SupporterName = supUser.UserName,
                        Rating = rating,
                        RatingMsg = comment,
                        Time = System.DateTime.Now
                    };
                    DbContext.RatingRecords.Add(model);
                }
                else
                {
                    model.Rating = rating;
                    model.RatingMsg = comment;
                    model.Time = System.DateTime.Now;
                    DbContext.RatingRecords.Update(model);
                }
                model.SupporterInfo = supUser;
                try
                {
                    await DbContext.SaveChangesAsync();
                    ViewData["IsSucceed"] = true;
                    ViewData["Msg"] = "提示：您的评论已成功提交~";
                }
                catch
                {
                    ViewData["IsSucceed"] = false;
                    ViewData["Msg"] = "在提交您的评论时出现了错误！";
                }
            }
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