using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIEnvironments
{
    public class MySampleActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            object topico;
            //  Tenta obter informações sobre o cenário e o tópico sendo testados...
            //  ... 1) Se foi passado o request header de identificação de cenário...
            if (context.HttpContext.Request.Headers.TryGetValue("X-Custom-Certification-Cenario", out var headerValue) &&
            //  ... 2) Se o valor que foi informado no "request header" equivale a um cenário válido (no enumerador)...
            Enum.TryParse<CenariosCertificacaoEnum>(headerValue, true, out var cenario) &&
            //  ... 3) Se existe um tópico de teste relacionado ao método da API requisitado...
            (topico = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.GetCustomAttributes(typeof(TopicoRelacionadoAttribute), true).FirstOrDefault()) != null &&
            //  ... 4) Se existe vínculo válido entre os cenário requisitado e o tópico associado ao método
            Cenario_x_Topico_Estao_Relacionados(cenario, (TopicoRelacionadoAttribute)topico))
            //  SUCESSO (vinculo entre cenário e tópico é valido): Executa o método que materializa os "mocks" dos serviços relacionados
                context.HttpContext.Response.Headers.Add("WWW-MyCustomRequestHeader", new[] { $"Basic:{DateTime.Now} - {cenario}" });
            //  FALHA (vínculo entre cenário e tópico inválido): finaliza a requisição com erro
            else
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            //MyDebug.Write(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        }
        static bool Cenario_x_Topico_Estao_Relacionados<TEnumCenario, TEnumTopico>(TEnumCenario cenario, TEnumTopico topico)
        {
            var customAttributtes = typeof(TEnumCenario).GetMember(cenario.ToString()).First().GetCustomAttributes(typeof(TEnumTopico), false);
            return customAttributtes.Count() > 0 && customAttributtes.Select(x => (TEnumTopico)x).First().Equals(topico);
        }

    }
}