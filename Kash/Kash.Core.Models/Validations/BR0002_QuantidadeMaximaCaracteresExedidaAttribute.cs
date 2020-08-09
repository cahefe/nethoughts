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
        protected override Func<object, ValidationContext, bool> ChackCondition => (value, validationContext) =>
            {
                Members = new string[] { nameof(value) };
                return value.ToString().Length <= MaxLength;
            };
    }
}