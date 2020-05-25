using Kash.Core.Models.Validations;
using Kash.Clients.WebAPI.Validations;

namespace Kash.Clients.WebAPI.DTO
{
    [BR0003_DTOSomaDeveTerValorMinimoAttribute(ParameterAttributeEnum.ValorMinimoVenda)]
    public class EntryReqDTO
    {
        public short ID { get; set; }
        [BR0001_ValorDeveSerMaiorQueParametro]
        public decimal Value { get; set; }
        [BR0001_ValorDeveSerMaiorQueParametro]
        public decimal NewValue { get; set; }
        [BR0002_QuantidadeMaximaCaracteresExedidaAttribute(5)]
        public string Texto{ get; set; }
    }
}