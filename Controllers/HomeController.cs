using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbBasicApp.Filters;
using Microsoft.AspNet.Mvc;

namespace DbBasicApp.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuth]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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

        [NonAction]
        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
        
        public IActionResult Status(int id)
        {
            ViewData["Status"] = id;
            return View("~/View/Shared/Status.cshtml");
        }
    }
}
