using System.Threading.Tasks;
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
            if(ModelState.IsValid)
            {
                var result = await _service.SignInAsync(model.Name, model.Password, model.RememberMe);
                if(result.IsSucceeded)
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
        
        public IActionResult Search()
        {
            return View();
        }
        
        private static void EnsureDatabaseCreated(AppDbContext context)
        {
            if(!_dbChecked)
            {
                _dbChecked = true;
                context.Database.Migrate();
            }
        }
        
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}