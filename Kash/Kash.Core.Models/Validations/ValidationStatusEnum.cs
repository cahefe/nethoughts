namespace Kash.Core.Models.Validations
{
    public enum ValidationBusinessRuleCodeEnum : int
    {
        [BusinessRuleDescription("Sucesso")]
        Success = 0,
        [BusinessRuleDescription("Valores menores ou iguais a zero não são aceitos")]
        ValuesLowerOrEqualZeroNotAccepted = 1
    }
}
