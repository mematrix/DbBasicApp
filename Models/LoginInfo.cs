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
        [Display(Name = "登录名")]
        public string UserName { get; set; }

        [Required, Column("password")]
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
}