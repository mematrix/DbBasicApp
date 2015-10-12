using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbBasicApp.Models
{
    // 用户详细信息表
    [Table("user_info")]
    public class UserInfo
    {
        [Key, Column("id")]
        public int ID { get; set; }

        [Required, Column("name")]
        //[RegularExpression(@"^([\u4e00-\u9fa5]{2,18})|((?!\s)[A-Za-z ]{0,30}[A-Za-z])$",
        //    ErrorMessage = "请输入正确的名称！")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Column("sex"), Display(Name = "性别")]
        public bool? Sex { get; set; }

        [Column("birthday"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "出生日期")]
        public System.DateTime? Birthday { get; set; }

        [Required, Column("card_id")]
        //[Remote("IsCardIDAvailable", "Validation")]
        [Display(Name = "身份证号码")]
        public string CardID { get; set; }

        [Required, Column("last_usage")]
        [Display(Name = "最近一次结算用量")]
        public double LastUsage { get; set; }

        [Required, Column("current_usage")]
        [Display(Name = "当前用量")]
        public double CurrentUsage { get; set; }

        [Required, Column("balance")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        [Display(Name = "账户余额")]
        public double Balance { get; set; }

        [Required, Column("reg_time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}")]
        [Display(Name = "注册时间")]
        public System.DateTime RegisterTime { get; set; }

        [Column("pkg_id")]
        public int? PackageID { get; set; }

        // 外键使用Fluent API配置
        // [ForeignKey("PackageID")]
        public virtual TelecomPackage TelPackage { get; set; }

        // public virtual UserLoginInfo UserLoginInfo { get; set; }

        // public virtual CashierLoginInfo CashierLoginInfo { get; set; }

        // public virtual SupporterLoginInfo SupporterLoginInfo { get; set; }
    }
}