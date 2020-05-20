using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Kash.Core.Models.Validations
{
    public abstract class ApplicationRuleValidationAttribute : ValidationAttribute
    {
        public readonly ValidationRuleEnum ValidationRule;
        public readonly ValidationBusinessRuleCodeEnum ValidationBusinessRuleCode;
        public new string ErrorMessage => ExtractBusinessRuleDescription();
        protected ApplicationRuleValidationAttribute(ValidationRuleEnum validationRule, ValidationBusinessRuleCodeEnum validationBusinessRudeCode) => (ValidationRule, ValidationBusinessRuleCode) = (validationRule, validationBusinessRudeCode);
        string ExtractBusinessRuleDescription()
        {
            BusinessRuleDescriptionAttribute attribute = default;
            MemberInfo memberInfo = typeof(ValidationBusinessRuleCodeEnum).GetMember(ValidationBusinessRuleCode.ToString()).FirstOrDefault();
            if (memberInfo != null && (attribute = (BusinessRuleDescriptionAttribute)memberInfo.GetCustomAttributes(typeof(BusinessRuleDescriptionAttribute), false).FirstOrDefault()) != default)
                    return attribute.Description;
            return "Business rule description not defined";
        }
    }
}