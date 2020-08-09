using System;

namespace Kash.Core.Models.Validations
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    class BusinessRuleExceptionDescriptionAttribute : Attribute
    {
        public readonly string Description;
        public BusinessRuleExceptionDescriptionAttribute(string description) => (Description) = (description);
    }
}