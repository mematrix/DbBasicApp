using System.ComponentModel.DataAnnotations;
using DbBasicApp.Validations;
using Microsoft.AspNet.Mvc;

namespace DbBasicApp.ViewModels
{
    public class ChargeViewModel
    {
        [Required(ErrorMessage = "用户名为必填项！")]
        [Remote("CheckUserNameExist", "Validation", HttpMethod = "Post", ErrorMessage = "该用户名不存在！")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "必须填写充值金额！")]
        [MinNumber(MinValue = 1, ErrorMessage = "最低充值金额必须为1元！")]
        [Display(Name = "充值金额")]
        public double Inpour { get; set; }
    }
}