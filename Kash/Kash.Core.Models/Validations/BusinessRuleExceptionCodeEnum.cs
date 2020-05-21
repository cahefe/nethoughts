namespace Kash.Core.Models.Validations
{
    public enum BusinessRuleExceptionCodeEnum : int
    {
        [BusinessRuleExceptionDescription("Sucesso")]
        Success = 0,
        [BusinessRuleExceptionDescription("Valores menores ou iguais a zero não são aceitos")]
        ValuesLowerOrEqualZeroNotAccepted = 1
    }
}
