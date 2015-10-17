using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using DbBasicApp.Filters;
using DbBasicApp.Models;
using DbBasicApp.Services;
using DbBasicApp.ViewModels;
using System.Linq;
using DbBasicApp.Util;

namespace DbBasicApp.Controllers
{
    public class AccountController : Controller
    {
        private AccountService _service;
        private AppDbContext _dbContext;

        public AccountController(AccountService service, AppDbContext context)
        {
            _service = service;
            _dbContext = context;
        }

        [CustomAuth]
        public async Task<IActionResult> Index(string id = null)
        {
            var info = await GetChildViewInfo(id);
            ViewData["Item"] = info.Name;
            ViewData["Model"] = info.Model;
            var user = await _service.GetCurrentUserAsync();
            return View(user);
        }

        [CustomAuth]
        public async Task<IActionResult> GetChildItem(string item)
        {
            var info = await GetChildViewInfo(item);
            return PartialView("~/Views/Partial/" + info.Name, info.Model);
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
            DbHelper.EnsureDatabaseCreated(_dbContext);
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
            DbHelper.EnsureDatabaseCreated(_dbContext);
            if (ModelState.IsValid)
            {
                if (await _dbContext.LoginInfos.AnyAsync(l =>
                    l.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("UserName", "用户名已存在！");
                    return View(model);
                }
                /* if (!Regex.IsMatch(model.CardID, @"^[1-9]\d{16}[\dxX]$"))
                {
                    ModelState.AddModelError("CardId", "请输入正确格式的身份证号码！");
                    return View(model);
                } */
                if (await _dbContext.UserInfos.AnyAsync(u =>
                    string.Equals(u.CardID, model.CardID, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("CardId", "您输入的身份证号码已存在！");
                    return View(model);
                }

                var result = await _service.RegisterAsync(model);
                if (result.IsSucceeded)
                {
                    await _service.SignInAsync(result.User);
                    return RedirectToAction(nameof(AccountController.Index));
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
            return RedirectToAction(nameof(AccountController.Login), new { returnUrl = "" });
        }

        public IActionResult Search()
        {
            return View();
        }

        public async Task<IActionResult> GetPublicInfo(string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return new EmptyResult();

            DbHelper.EnsureDatabaseCreated(_dbContext);
            var model = await _dbContext.UserInfos.FirstOrDefaultAsync(l => l.CardID == q);
            return PartialView("~/Views/Partial/PublicInfoView.cshtml", model);
        }

        [CustomAuth]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [CustomAuth]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _service.GetCurrentUserAsync();
                if (user.Password != model.CurrentPassword)
                {
                    ModelState.AddModelError("CurrentPassword", "您输入的密码有误！");
                    return View(model);
                }
                if (model.CurrentPassword == model.NewPassword)
                {
                    ModelState.AddModelError(string.Empty, "新密码不可以和原有密码一样！");
                    return View(model);
                }

                user.Password = model.NewPassword;
                _dbContext.LoginInfos.Update(user);
                await _dbContext.SaveChangesAsync();

                await _service.SignOutAsync();
                return RedirectToAction(nameof(AccountController.Login));
            }

            return View(model);
        }

        [CustomAuth]
        public async Task<IActionResult> EditInfo()
        {
            var userInfo = (await _service.GetCurrentUserAsync()).UserInfo;
            var model = new EditInfoViewModel
            {
                Name = userInfo.Name,
                Sex = userInfo.Sex.HasValue ? (userInfo.Sex.Value ? 1 : 2) : 0,
                Birthday = userInfo.Birthday,
                CardID = userInfo.CardID
            };
            return View(model);
        }

        [HttpPost]
        [CustomAuth]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfo(EditInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userInfo = (await _service.GetCurrentUserAsync()).UserInfo;
                if (model.CardID != userInfo.CardID)
                {
                    if (await _dbContext.UserInfos.AnyAsync(u =>
                        u.CardID.Equals(model.CardID, StringComparison.OrdinalIgnoreCase)))
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
                userInfo.Name = model.Name;
                userInfo.Sex = sex;
                userInfo.CardID = model.CardID;
                userInfo.Birthday = model.Birthday;
                await _service.UpdateInfoAsync(userInfo);
                return RedirectToAction(nameof(AccountController.Index), new { id = "UserInfoView" });
            }

            return View(model);
        }

        #region 辅助方法和内容

        private async Task<ChildViewInfo> GetChildViewInfo(string id, bool mapNullToDefault = true)
        {
            if (mapNullToDefault)
            {
                id = id ?? "LoginView";
            }
            var user = await _service.GetCurrentUserAsync();
            if (user == null || id == null)
            {
                return new ChildViewInfo { Model = null, Name = "Default.cshtml" };
            }

            object model = null;
            string name = null;
            switch (id.ToLower())
            {
                case "loginview":
                    name = "LoginView.cshtml";
                    model = user;
                    break;
                case "userinfoview":
                    name = "UserInfoView.cshtml";
                    model = user.UserInfo;
                    break;
                case "consumview":
                    name = "ConsumView.cshtml";
                    model = _dbContext.PaymentRecords.Where(p => p.UserName == user.UserName);
                    break;
                case "payview":
                    name = "PayView.cshtml";
                    model = _dbContext.PaymentRecords.Where(p => p.UserName == user.UserName && p.PayOut > 0);
                    break;
                case "deductview":
                    name = "DeductView.cshtml";
                    model = _dbContext.PaymentRecords.Where(p => p.UserName == user.UserName && p.PayOut < 0);
                    break;
                case "commentview":
                    name = "CommentView.cshtml";
                    model = _dbContext.RatingRecords.Where(r => r.UserName == user.UserName)
                        .OrderByDescending(r => r.Time)
                        .GroupBy(r => r.SupporterName)
                        .Select(g => g.First());
                    break;
                default: name = "Default.cshtml"; break;
            }
            return new ChildViewInfo { Model = model, Name = name };
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(AccountController.Index), "Account");
        }

        /// <summary>
        /// 描述子视图所需信息
        /// </summary>
        protected class ChildViewInfo
        {
            /// <summary>
            /// 获取或设置子视图数据模型
            /// </summary>
            public object Model { get; set; }

            /// <summary>
            /// 获取或设置子视图名称（包括扩展名但不包含完整路径）
            /// </summary>
            public string Name { get; set; }
        }

        #endregion
    }
}