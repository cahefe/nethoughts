using System.Text.Json.Serialization;

namespace WebAPIEnvironments.Models
{
    /// <summary>
    /// Representa todo o rol de cenários de certificação disponíveis, para serem relacionados à métodos da API para os testes
    /// </summary>
    /// <remarks>
    /// É importante que para cada item do enumerador tenha uma classe de teste de certificação com o mesmo nome (com exceção do item Indefinido)
    /// </remarks>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CenariosCertificacaoEnum
    {
        Indefinido = 0,
        Registrar_Investimento_Quantidade_Menor_Igual_Zero,
        Registrar_Investimento_Titulo_Inexistente,
        Registrar_Bloqueio_Titulo_Inexistente,
    }
}