using Microsoft.AspNet.Mvc;
using DbBasicApp.Models;
using System.Linq;
using System;

namespace DbBasicApp.Controllers
{
    public class ValidationController : Controller
    {
        [FromServices]
        public AppDbContext DbContext { get; set; }

        [HttpPost]
        public JsonResult IsCardIDExisted(string cardId, string oldId = null)
        {
            if (string.IsNullOrWhiteSpace(cardId) || cardId == oldId)
            {
                return Json(true);
            }
            if (DbContext.UserInfos.Any(u => string.Equals(u.CardID, cardId, StringComparison.OrdinalIgnoreCase)))
                return Json(false);
            return Json(true);
        }

        [HttpPost]
        public JsonResult IsUserNameExisted(string userName)
        {
            if (DbContext.LoginInfos.Any(l => string.Equals(l.UserName, userName, StringComparison.OrdinalIgnoreCase)))
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}