using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAPIEnvironments
{
    /*  *** Referências ***
        - Tratando requestes e responses em um middleware: https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/
        - Dica: obtendo o "Route Data" (Stack overflow): https://stackoverflow.com/questions/39335824/route-controller-and-action-in-middleware
        - Sugestão: usar "Filter": https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-5.0
    */
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TopicosCenariosCertificacaoEnum
    {
        Negociacao,
        Bloqueios,
        Transferencias,
        Notificacoes
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CenariosCertificacaoEnum
    {
        [TopicoRelacionado(TopicosCenariosCertificacaoEnum.Negociacao)]
        Registrar_Investimento_Quantidade_Menor_Igual_Zero,
        [TopicoRelacionado(TopicosCenariosCertificacaoEnum.Negociacao)]
        Registrar_Investimento_Titulo_Inexistente,
        [TopicoRelacionado(TopicosCenariosCertificacaoEnum.Bloqueios)]
        Registrar_Bloqueio_Titulo_Inexistente,
    }
    public class CenariosCertificacao
    {
        //  Leia referência de middlware: e

        readonly RequestDelegate _next;
        public CenariosCertificacao(RequestDelegate next) => (_next) = (next);
        public async Task InvokeAsync(HttpContext context)   //, IAuthenticationService authenticationService
        {
            //  Tenta obter o cenário a ser testado
            if (context.Request.Headers.TryGetValue("X-Custom-Certification-Cenario", out var headerValue) && Enum.TryParse<CenariosCertificacaoEnum>(headerValue, true, out var cenario))
            {
                context.Response.Headers.Add("WWW-MyCustomRequestHeader", new[] { $"Basic:{DateTime.Now} - {cenario}" });
                await _next(context);
            }
            //  ... caso contrário, finaliza a requisição com erro
            else
                context.Response.StatusCode = StatusCodes.Status409Conflict;
        }

        static Dictionary<TopicosCenariosCertificacaoEnum, string> cenarios;

        static CenariosCertificacao()
        {
            cenarios = new Dictionary<TopicosCenariosCertificacaoEnum, string>();
            cenarios.Add(TopicosCenariosCertificacaoEnum.Negociacao, "Tema1:Teste1");
            cenarios.Add(TopicosCenariosCertificacaoEnum.Bloqueios, "Tema1:Teste2");
        }

        static string ObterInfo(TopicosCenariosCertificacaoEnum topicoCenario) => cenarios[topicoCenario];
    }
}