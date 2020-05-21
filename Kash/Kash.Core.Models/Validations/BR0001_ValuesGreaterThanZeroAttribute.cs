using System;

namespace Kash.Core.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BR0001_ValuesGreaterThanZeroAttribute : BusinessRuleValidationAttribute
    {
        public BR0001_ValuesGreaterThanZeroAttribute() : base(BusinessRuleEnum.ValuesMustBeGreaterOrEqualZero, BusinessRuleExceptionCodeEnum.ValuesLowerOrEqualZeroNotAccepted) { }
        public override bool IsValid(object value) => (decimal)value > 0;
    }
}