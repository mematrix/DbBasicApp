using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Mvc;

namespace DbBasicApp.Models
{
    // 用户详细信息表
    [Table("user_info")]
    public class UserInfo
    {
        [Key, Column("id")]
        public int ID { get; set; }

        [Required(ErrorMessage = "姓名为必填项"), Column("name")]
        [RegularExpression(@"^([\u4e00-\u9fa5]{2,18})|((?!\s)[A-Za-z ]{0,30}[A-Za-z])$",
            ErrorMessage = "请输入正确的名称！")]
        public string Name { get; set; }

        [Column("sex")]
        public bool? Sex { get; set; }

        [Column("birthday"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime? Birthday { get; set; }

        [Column("card_id")]
        [Remote("IsCardIDAvailable", "Validation")]
        public string CardID { get; set; }

        [Required, Column("last_usage")]
        public int LastUsage { get; set; }

        [Required, Column("current_usage")]
        public int CurrentUsage { get; set; }

        [Required, Column("balance")]
        public int Balance { get; set; }

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