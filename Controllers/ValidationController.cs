using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using DbBasicApp.Models;
using DbBasicApp.Services;

namespace DbBasicApp.Controllers
{
    public class ValidationController : Controller
    {
        [FromServices]
        public AppDbContext DbContext { get; set; }

        [FromServices]
        public AccountService Service { get; set; }

        [HttpPost]
        public async Task<JsonResult> IsCardIDExisted(string cardId)
        {
            if (await DbContext.UserInfos.AnyAsync(u => u.CardID.Equals(cardId, StringComparison.OrdinalIgnoreCase)))
                return Json(false);
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> IsCardIDAvailable(string cardId)
        {
            var user = await Service.GetCurrentUserAsync();
            if (user == null || (user != null && user.UserInfo.CardID == cardId))
            {
                return Json(true);
            }

            if (await DbContext.UserInfos.AnyAsync(u => u.CardID.Equals(cardId, StringComparison.OrdinalIgnoreCase)))
            {
                return Json(false);
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> IsUserNameExisted(string userName)
        {
            if (await DbContext.LoginInfos.AnyAsync(l => l.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)))
            {
                return Json(false);
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> CheckUserNameExist(string userName)
        {
            if (await DbContext.LoginInfos.AnyAsync(l => l.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)))
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}