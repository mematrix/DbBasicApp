using System;
using System.ComponentModel.DataAnnotations;

namespace DbBasicApp.Validations
{
    // TODO: 不等于的情况验证
    // 验证属性／字段／参数等的值不小于指定的值（仅限于数值）
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class MinNumberAttribute : ValidationAttribute
    {
        public double MinValue { get; set; }

        public override bool IsValid(object value)
        {
            double v = 0.0;
            return double.TryParse(value as string, out v) && v >= MinValue;
        }
    }
}