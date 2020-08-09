using System;
using System.ComponentModel.DataAnnotations;

namespace Kash.Core.Models.Validations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class BR0003_SomaDeveTerValorMinimoAttribute : BusinessRuleValidationAttribute
    {
        public readonly decimal MinValue;
        public BR0003_SomaDeveTerValorMinimoAttribute(int minValue = 0) : base(BusinessRuleExceptionCodeEnum.SumMustHaveMinimumValue)
        {
            MinValue = (decimal)minValue;
            ResultArgs = new object[] { MinValue };
        }
        protected override Func<object, ValidationContext, bool> ChackCondition => (value, validationContext) =>
            {
                var entry = (Entry)value;
                Members = new string[] { nameof(entry.Value), nameof(entry.ExtraValue), nameof(entry.FeesValue), nameof(entry.TicketValue) };
                return entry.Value + entry.ExtraValue + entry.FeesValue + entry.TicketValue >= MinValue;
            };
    }
}