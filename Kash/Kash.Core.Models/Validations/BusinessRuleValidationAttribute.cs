using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Kash.Core.Models.Validations
{
    public abstract class BusinessRuleValidationAttribute : ValidationAttribute
    {
        //  TODO: Generalizar a função, minimizando recursos: https://docs.microsoft.com/pt-br/dotnet/api/system.func-2?view=netcore-3.1
        Func<object, ValidationContext, bool> Rule;
        protected object[] ResultArgs { get; set; }
        protected IEnumerable<string> Members { get; set; }
        protected abstract bool RuleImplementation(object value, ValidationContext validationContext);
        public readonly BusinessRuleExceptionCodeEnum ValidationBusinessRuleCode;
        // public new string ErrorMessage => ExtractBusinessRuleDescription();
        public new string ErrorMessage { get; private set; }
        public int Code => (int)ValidationBusinessRuleCode;
        protected BusinessRuleValidationAttribute(BusinessRuleExceptionCodeEnum validationBusinessRudeCode)
        {
            ValidationBusinessRuleCode = validationBusinessRudeCode;
            ErrorMessage = ExtractBusinessRuleDescription();
            Rule = RuleImplementation;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Rule(value, validationContext))
                return ValidationResult.Success;
            return new ValidationResult(MixedErrorMesssage(ResultArgs), Members);
        }
        string MixedErrorMesssage(params object[] args) => ErrorMessage = string.Format(ExtractBusinessRuleDescription(), args);
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