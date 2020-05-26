using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Kash.Core.Models.Validations
{
    public abstract class BusinessRuleValidationAttribute : ValidationAttribute
    {
        public readonly BusinessRuleExceptionCodeEnum ValidationBusinessRuleCode;
        public readonly int Code;
        protected abstract Func<object, ValidationContext, bool> ChackCondition { get; }
        protected object[] ResultArgs { get; set; }
        protected string[] Members { get; set; }
        // public new string ErrorMessage { get; private set; }
        protected BusinessRuleValidationAttribute(BusinessRuleExceptionCodeEnum validationBusinessRudeCode)
        {
            ValidationBusinessRuleCode = validationBusinessRudeCode;
            Code = (int)ValidationBusinessRuleCode;
            ErrorMessage = ExtractBusinessRuleDescription();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ChackCondition(value, validationContext))
                return ValidationResult.Success;
            return new ValidationResult(MixedErrorMesssage(ResultArgs), Members.Select(m => m.Replace(".value", "")));
        }
        string MixedErrorMesssage(params object[] args) => ErrorMessage = ExtractBusinessRuleDescription();
        string ExtractBusinessRuleDescription()
        {
            BusinessRuleExceptionDescriptionAttribute attribute = default;
            MemberInfo memberInfo = typeof(BusinessRuleExceptionCodeEnum).GetMember(ValidationBusinessRuleCode.ToString()).FirstOrDefault();
            if (memberInfo != null && (attribute = (BusinessRuleExceptionDescriptionAttribute)memberInfo.GetCustomAttributes(typeof(BusinessRuleExceptionDescriptionAttribute), false).FirstOrDefault()) != default)
                return attribute.Description + " [" + ((int)ValidationBusinessRuleCode) + ": " + ValidationBusinessRuleCode + "]";
            return "Business rule description not defined";
        }
    }
}