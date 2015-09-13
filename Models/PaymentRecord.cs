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
        public int PayOut{get;set;}
        
        [Required, Column("time")]
        public System.DateTime Time{get;set;}
        
        [Column("msg")]
        public string Msg{get;set;}
        
        [Required, Column("user_name")]
        public string UserName{get;set;}
        
        [Required, ForeignKey("UserName")]
        public virtual UserLoginInfo UserLoginInfo{get;set;}
        
        [Column("cashier_name")]
        public string CashierName{get;set;}
        
        [ForeignKey("CashierName")]
        public virtual CashierLoginInfo CashierInfo{get;set;}
    }
}