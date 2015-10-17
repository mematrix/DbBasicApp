using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;

namespace DbBasicApp.Validations
{
    // TODO: 不等于的情况验证
    // 验证属性／字段／参数等的值不小于指定的值（仅限于数值）
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class MinNumberAttribute : ValidationAttribute, IClientModelValidator 
    {
        public double MinValue { get; set; } = 0;

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ClientModelValidationContext context)
        {
            var rule = new ModelClientValidationRule("minnumber", 
                this.FormatErrorMessage(context.ModelMetadata.DisplayName));
            rule.ValidationParameters.Add("minvalue", MinValue);
            yield return rule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            try
            {
                var num = Convert.ToDouble(value);
                if (num >= MinValue)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(this.FormatErrorMessage(context.DisplayName));
            }
            catch
            {
                return new ValidationResult(this.FormatErrorMessage(context.DisplayName));
            }
        }
    }
}