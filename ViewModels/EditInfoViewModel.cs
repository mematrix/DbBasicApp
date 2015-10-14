using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc;

namespace DbBasicApp.ViewModels
{
    public class EditInfoViewModel
    {
        [Required(ErrorMessage = "姓名为必填项！")]
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

        [Required(ErrorMessage = "请输入您的身份证号码！")]
        [RegularExpression(@"^[1-9]\d{16}[\dX]$", ErrorMessage = "请输入正确格式的身份证号码！")]
        [Remote("IsCardIDAvailable", "Validation", HttpMethod = "Post", ErrorMessage = "您输入的身份证号码已存在！")]
        [Display(Name = "身份证号码")]
        public string CardID { get; set; }
    }
}