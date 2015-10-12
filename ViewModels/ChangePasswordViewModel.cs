using System.ComponentModel.DataAnnotations;

namespace DbBasicApp.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "请输入当前密码！")]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "请输入新的密码！")]
        [RegularExpression(@"^\w{6,25}$", ErrorMessage = "密码只能由字母、数字及下划线组成，长度在6到25位之间。")]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "验证密码不正确！")]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }
    }
}