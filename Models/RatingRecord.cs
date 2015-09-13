using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbBasicApp.Models
{
    [Table("rating_record")]
    public class RatingRecord
    {
        [Key, Column("id")]
        public int ID { get; set; }

        [Required(ErrorMessage = "请选择一个分数"), Column("rating")]
        [Range(1, 5, ErrorMessage = "数值大小超出索引范围")]
        public int Rating { get; set; }

        [Column("rating_msg")]
        public string RatingMsg { get; set; }

        [Required, Column("user_name")]
        public string UserName { get; set; }

        [Required, ForeignKey("UserName")]
        public virtual UserLoginInfo UserLoginInfo { get; set; }

        [Required, Column("supporter_name")]
        public string SupporterName { get; set; }

        [Required, ForeignKey("SupporterName")]
        public SupporterLoginInfo SupporterInfo { get; set; }
    }
}