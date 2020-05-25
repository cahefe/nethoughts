using System;
using System.ComponentModel.DataAnnotations;

namespace Kash.Core.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BR0002_QuantidadeMaximaCaracteresExedidaAttribute : BusinessRuleValidationAttribute
    {
        public readonly int MaxLength;
        public BR0002_QuantidadeMaximaCaracteresExedidaAttribute(int maxLength) : base(BusinessRuleExceptionCodeEnum.StringMaxLengthExceded)
        {
            MaxLength = maxLength;
            ResultArgs = new object[] { MaxLength };
        }
        protected override bool RuleImplementation(object value, ValidationContext validationContext)
        {
            Members = new[] { nameof(value) };
            return value.ToString().Length <= MaxLength;
        }
    }
}