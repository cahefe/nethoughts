using System;

namespace Kash.Core.Models.Validations
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    class BusinessRuleDescriptionAttribute : Attribute
    {
        public readonly string Description;
        public BusinessRuleDescriptionAttribute(string description) => (Description) = (description);
    }
}