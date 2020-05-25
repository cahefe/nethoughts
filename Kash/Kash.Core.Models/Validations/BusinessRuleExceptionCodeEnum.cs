namespace Kash.Core.Models.Validations
{
    public enum BusinessRuleExceptionCodeEnum : int
    {
        [BusinessRuleExceptionDescription("Sucesso")]
        Success = 0,
        [BusinessRuleExceptionDescription("Valor deve ser maior que {0}")]
        ValueMustBeGreaterThanMinimum = 1,
        [BusinessRuleExceptionDescription("Quantidade máxima de caracteres ({0}) excedida")]
        StringMaxLengthExceded = 2,
        [BusinessRuleExceptionDescription("A soma dos termos deve ter um valor mínimo de {0}")]
        SumMustHaveMinimumValue = 3
    }
}
