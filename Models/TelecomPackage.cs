using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DbBasicApp.Validations;

namespace DbBasicApp.Models
{
    // 电信套餐表
    [Table("telecom_pkg")]
    public class TelecomPackage
    {
        [Key, Column("id")]
        public int ID { get; set; }

        [Required(ErrorMessage = "套餐名称不可为空！"), Column("name")]
        [RegularExpression(@"^(?!\s).*\S+$", ErrorMessage = "请输入正确的名称，首尾不要包含空白符。")]
        public string Name { get; set; }

        [Required(ErrorMessage = "必须指定套餐价格！"), Column("price")]
        [MinNumber(MinValue = 0, ErrorMessage = "请输入正确的定价！")]
        public int Price { get; set; }

        [Required(ErrorMessage = "必须指定套餐内包含用量！"), Column("base_usage")]
        [MinNumber(ErrorMessage = "请输入正确的数值！")]
        public int BaseUsage { get; set; }

        [Required(ErrorMessage = "必须指定超出套餐外的单价！"), Column("out_price")]
        [MinNumber(ErrorMessage = "请输入正确的数值！")]
        public int OutPrice { get; set; }

        public virtual List<UserInfo> UserInfos { get; set; }
    }
}