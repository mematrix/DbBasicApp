using Microsoft.AspNet.Mvc;
using System.Text.RegularExpressions;

namespace DbBasicApp.Controllers
{
    public class ValidationController : Controller
    {
        public JsonResult IsCardIDAvailable(string cardId)
        {
            if(cardId.Length==0)
            {
                return Json(true);
            }
            if(Regex.IsMatch(cardId,@"^[1-9]\d{16}[\dxX]$"))
            {
                return Json("输入正确✅");
            }
            return Json("请输入正确的身份证号码！");
        }
    }
}