using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbBasicApp.Models
{
    // 缴费及扣费信息表
    [Table("payment_record")]
    public class PaymentRecord
    {
        [Key, Column("id")]
        public int ID { get; set; }

        [Required, Column("pay_out")]
        public double PayOut { get; set; }

        [Required, Column("time")]
        public System.DateTime Time { get; set; }

        [Column("msg")]
        public string Msg { get; set; }

        [Required, Column("user_name")]
        public string UserName { get; set; }

        // 外键使用Fluent API配置
        // [Required, ForeignKey("UserName")]
        public virtual LoginInfo UserLoginInfo { get; set; }

        [Column("cashier_name")]
        public string CashierName { get; set; }

        // 外键使用Fluent API配置
        // [ForeignKey("CashierName")]
        public virtual LoginInfo CashierInfo { get; set; }
    }
}