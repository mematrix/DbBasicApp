using System.ComponentModel.DataAnnotations;

namespace DbBasicApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="登录名不可为空！")]
        [Display(Name = "登录名")]
        public string UserName { get; set; }

        [Required(ErrorMessage="密码不可为空！")]
        [DataType(DataType.Password)]
        [Display(Name = "登录密码")]
        public string Password { get; set; }
        
        [Display(Name = "记住我")]
        public bool RememberMe{get;set;}
    }
}