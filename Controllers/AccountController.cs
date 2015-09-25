using DbBasicApp.Filters;
using DbBasicApp.ViewModels;
using Microsoft.AspNet.Mvc;

namespace DbBasicApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                
            }
            return View();
        }
        
        public IActionResult Register()
        {
            return View();
        }
        
        [CustomAuth]
        public IActionResult Search()
        {
            return View();
        }
    }
}