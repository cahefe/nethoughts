namespace Kash.Core.Models.Validations
{
    public enum BusinessRuleEnum : int
    {
        Undefined = 0,
        ValueMustBeGreaterThanZero = 1,
        ValuesMustBeGreaterOrEqualZero = 2,
        DocumentCPFInCorrectFormat = 3,
    }
}
