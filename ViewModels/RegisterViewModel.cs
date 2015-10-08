using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc;

namespace DbBasicApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="登录名不可为空！")]
        [RegularExpression(@"^[\w\u4e00-\u9fa5]{3,30}$",
            ErrorMessage = "登录名只能由汉字、数字、字母及下划线组成，并且长度在3到30之间。")]
        [Remote("IsUserNameExisted", "Validation", HttpMethod = "Post", ErrorMessage = "用户名已存在！")]
        [Display(Name = "登录名")]
        public string UserName { get; set; }

        [Required(ErrorMessage="登录密码不可为空！")]
        [RegularExpression(@"^\w{6,25}$", ErrorMessage = "密码只能由字母、数字及下划线组成，长度在6到25位之间。")]
        [DataType(DataType.Password)]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "验证密码不正确！")]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage="姓名不可为空！")]
        [RegularExpression(@"^([\u4e00-\u9fa5]{2,18})|((?!\s)[A-Za-z ]{0,30}[A-Za-z])$",
            ErrorMessage = "请输入正确的名称！")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 性别：0[default]：未指定，1：男，2：女
        /// </summary>
        [Display(Name = "性别")]
        public int Sex { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "出生日期")]
        public System.DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "身份证号码必须填写！")]
        [RegularExpression(@"^[1-9]\d{16}[\dX]$", ErrorMessage = "请输入正确格式的身份证号码！")]
        [Remote("IsCardIDExisted", "Validation", HttpMethod = "Post", ErrorMessage = "您输入的身份证号码已存在！")]
        [Display(Name = "身份证号码")]
        public string CardID { get; set; }
    }
}