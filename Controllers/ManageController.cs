using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using DbBasicApp.Filters;
using DbBasicApp.Models;
using DbBasicApp.Services;
using DbBasicApp.ViewModels;

namespace DbBasicApp.Controllers
{
    public class ManageController : Controller
    {
        [FromServices]
        public AccountService Service { get; set; }

        [FromServices]
        public AppDbContext DbContext { get; set; }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(ManageController.Charge));
        }

        [Authentic]
        public IActionResult Charge()
        {
            return View();
        }
        
        [HttpPost]
        [Authentic]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Charge(ChargeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!(await DbContext.LoginInfos.AnyAsync(l=>l.UserName == model.UserName)))
                {
                    ModelState.AddModelError("UserName", "该用户名不存在！");
                    return View(model);
                }
                
                var user = await Service.GetCurrentUserAsync();
                DbContext.PaymentRecords.Add(new PaymentRecord
                {
                    PayOut = model.Inpour,
                    Time = System.DateTime.Now,
                    UserName = model.UserName,
                    CashierName = user.UserName
                });
                user.UserInfo.Balance += model.Inpour;
                DbContext.UserInfos.Update(user.UserInfo);
                await DbContext.SaveChangesAsync();
                return RedirectToAction(nameof(ManageController.Charge));
            }
            
            return View(model);
        }

        [Authentic]
        public IActionResult AddPackage()
        {
            return View();
        }
        
        [HttpPost]
        [Authentic]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPackage([Bind("Name", "Price", "BaseUsage", "OutPrice")]TelecomPackage model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DbContext.TelePackages.Add(model);
                    await DbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                catch
                {
                    // 500 - 服务器出现错误
                    return new HttpStatusCodeResult(500);
                }
            }
            return View(model);
        }

        [Authentic]
        public async Task<IActionResult> EditPackage(int id)
        {
            var model = await DbContext.TelePackages.FirstOrDefaultAsync(t => t.ID == id);
            if (model == null)
            {
                // 404 - 资源不存在
                return new HttpStatusCodeResult(404);
            }
            return View(model);
        }

        [HttpPost]
        [Authentic]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPackage(TelecomPackage model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DbContext.TelePackages.Update(model);
                    await DbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                catch
                {
                    // 416 - 无效的请求值
                    return new HttpStatusCodeResult(416);
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authentic]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePackage(int id)
        {
            try
            {
                var toDel = new TelecomPackage { ID = id };
                DbContext.TelePackages.Attach(toDel);
                DbContext.TelePackages.Remove(toDel);
                await DbContext.SaveChangesAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch
            {
                // 416 - 无效的请求值
                return new HttpStatusCodeResult(416);
            }
        }
    }
}