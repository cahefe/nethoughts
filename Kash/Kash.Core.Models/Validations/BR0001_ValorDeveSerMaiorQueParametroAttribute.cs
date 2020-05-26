using System;
using System.ComponentModel.DataAnnotations;

namespace Kash.Core.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BR0001_ValorDeveSerMaiorQueParametroAttribute : BusinessRuleValidationAttribute
    {
        public readonly decimal MinValue;
        public BR0001_ValorDeveSerMaiorQueParametroAttribute(int minValue = 0) : base(BusinessRuleExceptionCodeEnum.ValueMustBeGreaterThanMinimum)
        {
            MinValue = (decimal)minValue;
            ResultArgs = new object[] { MinValue };
        }
        protected override Func<object, ValidationContext, bool> ChackCondition => (value, validationContext) =>
            {
                Members = new string[] { nameof(value) };
                return (decimal)value > 0;
            };
    }
}