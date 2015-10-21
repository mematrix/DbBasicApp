using System.Collections.Generic;
using DbBasicApp.Models;
using Microsoft.AspNet.Mvc.Rendering;

namespace DbBasicApp.ViewModels
{
    public class ChangeTelPackageViewModel
    {
        public int PkgID {get;set;}
        
        public IList<SelectListItem> ListItems {get;set;}
        
        public IList<TelecomPackage> Packages {get;set;}
    }
}