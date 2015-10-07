using System;
using System.Threading.Tasks;
using DbBasicApp.Filters;
using DbBasicApp.Models;
using DbBasicApp.Services;
using DbBasicApp.ViewModels;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace DbBasicApp.Controllers
{
    public class AccountController : Controller
    {
        private AccountService<LoginInfo> _service;
        private AppDbContext _context;
        private static bool _dbChecked;

        public AccountController(AccountService<LoginInfo> service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            EnsureDatabaseCreated(_context);
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _service.SignInAsync(model.UserName, model.Password, model.RememberMe);
                if (result.IsSucceeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "登录失败，请检查账号密码是否正确。");
            }
            model.Password = null;
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            EnsureDatabaseCreated(_context);
            if (ModelState.IsValid)
            {
                if (await _context.LoginInfos.AnyAsync(l =>
                     l.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("UserName", "用户名已存在！");
                    return View(model);
                }
                if (!string.IsNullOrWhiteSpace(model.CardID))
                {
                    model.CardID = model.CardID.Trim();
                    /* if (!Regex.IsMatch(model.CardID, @"^[1-9]\d{16}[\dxX]$"))
                    {
                        ModelState.AddModelError("CardId", "请输入正确格式的身份证号码！");
                        return View(model);
                    } */
                    if (await _context.UserInfos.AnyAsync(u =>
                         string.Equals(u.CardID, model.CardID, StringComparison.OrdinalIgnoreCase)))
                    {
                        ModelState.AddModelError("CardId", "您输入的身份证号码已存在！");
                        return View(model);
                    }
                }

                bool? sex = null;
                if (model.Sex == 1)
                {
                    sex = true;
                }
                else if (model.Sex == 2)
                {
                    sex = false;
                }
                var userInfo = new UserInfo
                {
                    Name = model.Name,
                    Sex = sex,
                    Birthday = model.Birthday,
                    CardID = model.CardID,
                    LastUsage = 0,
                    CurrentUsage = 0,
                    Balance = 0,
                    RegisterTime = DateTime.Now,
                    TelPackage = null
                };
                var user = new LoginInfo
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Level = 0,
                    UserInfo = userInfo
                };
                var result = await _service.RegisterAsync(user);
                if (result.IsSucceeded)
                {
                    await _service.SignInAsync(user);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                ModelState.AddModelError(string.Empty, "注册失败！请检查您的注册信息是否正确。");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuth]
        public async Task<IActionResult> LogOff()
        {
            await _service.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login));
        }

        public IActionResult Search()
        {
            return View();
        }

        private static void EnsureDatabaseCreated(AppDbContext context)
        {
            if (!_dbChecked)
            {
                _dbChecked = true;
                context.Database.Migrate();
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}