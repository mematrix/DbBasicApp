using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbBasicApp.Models
{
    // 客服服务消息表
    [Table("msg_record")]
    public class MsgRecord
    {
        [Key, Column("id")]
        public int ID { get; set; }

        [Required, Column("msg")]
        public string Msg { get; set; }

        [Required, Column("time")]
        public System.DateTime Time { get; set; }

        [Required, Column("sender_name")]
        public string SenderName { get; set; }

        // 使用Fluent API配置外键关系
        public virtual LoginInfo SenderLoginInfo { get; set; }

        [Required, Column("recv_name")]
        public string ReceiverName { get; set; }

        // 使用Fluent API配置外键关系
        public virtual LoginInfo RecvLoginInfo { get; set; }
    }
}