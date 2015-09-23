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
        [Display(Name = "评分")]
        public int Rating { get; set; }

        [Column("rating_msg"), DataType(DataType.MultilineText)]
        [Display(Name = "评价内容")]
        public string RatingMsg { get; set; }

        [Required, Column("time")]
        public System.DateTime Time { get; set; }

        [Required, Column("user_name")]
        public string UserName { get; set; }

        // 外键使用Fluent API配置
        // [Required, ForeignKey("UserName")]
        public virtual LoginInfo UserLoginInfo { get; set; }

        [Required, Column("supporter_name")]
        [Display(Name = "客服人员")]
        public string SupporterName { get; set; }

        // 外键使用Fluent API配置
        // [Required, ForeignKey("SupporterName")]
        public virtual LoginInfo SupporterInfo { get; set; }
    }
}