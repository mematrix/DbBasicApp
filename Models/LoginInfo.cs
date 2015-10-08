// using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DbBasicApp.Services;

namespace DbBasicApp.Models
{
    // 登录信息表
    [Table("login_info")]
    public class LoginInfo : ISignInInfo
    {
        [Key, Column("user_name")]
        //[RegularExpression(@"^[\w\u4e00-\u9fa5]{3,30}$",
        //    ErrorMessage = "登录名只能由汉字、数字、字母及下划线组成，并且长度在3到30之间。")]
        [Display(Name = "登录名")]
        public string UserName { get; set; }

        [Required, Column("password")]
        //[RegularExpression(@"^\w{6,25}$", ErrorMessage = "密码只能由字母、数字及下划线组成，长度在6到25位之间。")]
        [DataType(DataType.Password)]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        // 用户权限级别：0普通用户，1客服人员，2收款员
        [Required, Column("level")]
        [Display(Name = "用户身份")]
        public int Level { get; set; }

        [Required, Column("user_id")]
        public int UserId { get; set; }

        // 外键使用Fluent API配置
        // [Required, ForeignKey("UserId")]
        public virtual UserInfo UserInfo { get; set; }
    }

    /*
    // 普通用户登录信息表
    [Table("user_login_info")]
    public class UserLoginInfo : LoginInfo
    {
        // public virtual PaymentRecord PaymentRecord { get; set; }

        public virtual List<RatingRecord> RatingRecords { get; set; }
    }

    // 收款员登录信息表
    [Table("cashier_login_info")]
    public class CashierLoginInfo : LoginInfo
    {
        public virtual List<PaymentRecord> PaymentRecords { get; set; }
    }

    // 客服登录信息表
    [Table("supporter_login_info")]
    public class SupporterLoginInfo : LoginInfo
    {
        public virtual List<RatingRecord> RatingRecords { get; set; }
    }
    */
}