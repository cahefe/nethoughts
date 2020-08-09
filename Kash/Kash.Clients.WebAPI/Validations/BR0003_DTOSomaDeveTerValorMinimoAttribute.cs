using System;
using System.ComponentModel.DataAnnotations;
using Kash.Clients.WebAPI.DTO;
using Kash.Core.Models.Validations;

namespace Kash.Clients.WebAPI.Validations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class BR0003_DTOSomaDeveTerValorMinimoAttribute : BusinessRuleValidationAttribute
    {
        public readonly decimal MinValue;
        public BR0003_DTOSomaDeveTerValorMinimoAttribute() : base(BusinessRuleExceptionCodeEnum.SumMustHaveMinimumValue)
        {
            MinValue = 0;
            ResultArgs = new object[] { MinValue };
        }
        public BR0003_DTOSomaDeveTerValorMinimoAttribute(ParameterAttributeEnum parameterMinValue) : base(BusinessRuleExceptionCodeEnum.SumMustHaveMinimumValue)
        {
            MinValue = parameterMinValue.Equals(ParameterAttributeEnum.ValorMinimoCompra) ? 5.1m : 2.9m;
            ResultArgs = new object[] { MinValue };
        }
        protected override Func<object, ValidationContext, bool> ChackCondition => (value, validationContext) =>
            {
                var entry = (EntryReqDTO)value;
                Members = new string[] { nameof(entry.Value), nameof(entry.NewValue) };
                return entry.Value + entry.NewValue >= MinValue;
            };
    }
}