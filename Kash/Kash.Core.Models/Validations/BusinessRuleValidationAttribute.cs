using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Kash.Core.Models.Validations
{
    public abstract class BusinessRuleValidationAttribute : ValidationAttribute
    {
        public readonly BusinessRuleEnum ValidationRule;
        public readonly BusinessRuleExceptionCodeEnum ValidationBusinessRuleCode;
        public new string ErrorMessage => ExtractBusinessRuleDescription();
        public int Code => (int)ValidationBusinessRuleCode;
        protected BusinessRuleValidationAttribute(BusinessRuleEnum validationRule, BusinessRuleExceptionCodeEnum validationBusinessRudeCode) => (ValidationRule, ValidationBusinessRuleCode) = (validationRule, validationBusinessRudeCode);
        string ExtractBusinessRuleDescription()
        {
            BusinessRuleExceptionDescriptionAttribute attribute = default;
            MemberInfo memberInfo = typeof(BusinessRuleExceptionCodeEnum).GetMember(ValidationBusinessRuleCode.ToString()).FirstOrDefault();
            if (memberInfo != null && (attribute = (BusinessRuleExceptionDescriptionAttribute)memberInfo.GetCustomAttributes(typeof(BusinessRuleExceptionDescriptionAttribute), false).FirstOrDefault()) != default)
                    return attribute.Description;
            return "Business rule description not defined";
        }
    }
}