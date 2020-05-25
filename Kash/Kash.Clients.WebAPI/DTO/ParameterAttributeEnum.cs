using Kash.Core.Models.Validations;
using Kash.Clients.WebAPI.Validations;

namespace Kash.Clients.WebAPI.DTO
{
    public enum ParameterAttributeEnum : byte
    {
        //  Exemplo de parâmetros existente em repósitorio
        //  Valor deve ser obtido e convertido no repositório
        //  Ou a partir de anotações nos enumeradores descrevento os tipos dos parâmetros
        ValorMinimoCompra = 1,
        ValorMinimoVenda = 2,
    }
}