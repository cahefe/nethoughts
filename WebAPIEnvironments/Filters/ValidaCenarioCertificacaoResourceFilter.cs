using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIEnvironments.Attributes;
using WebAPIEnvironments.Interfaces;
using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Filters
{
    /// <summary>
    /// Filtro valida a disponibilidade do Request-Header "X-Custom-Certification-Cenario" com o valor do cenário a ser executado
    /// </summary>
    public class ValidaCenarioCertificacaoResourceFilter : IResourceFilter
    {
        const string HEADER_CERTIFICACAO = "X-Custom-Certification-Cenario";
        readonly ICertificationTestService _certificationService;
        public ValidaCenarioCertificacaoResourceFilter(ICertificationTestService certificationService) => _certificationService = certificationService;
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //  Tenta obter informações sobre o cenário e o tópico sendo testados...
            //  ... 1) Se foi passado o request header de identificação de cenário...
            if (context.HttpContext.Request.Headers.TryGetValue(HEADER_CERTIFICACAO, out var headerValue) &&
            //  ... 2) Se o valor que foi informado no "request header" equivale a um cenário válido (no enumerador)...
            Enum.TryParse<CenariosCertificacaoEnum>(headerValue, true, out var cenario) &&
            //  ... 3) Se existe vínculo válido entre o cenário requisitado e método da API
            Cenario_x_Metodo_Estao_Relacionados(context, cenario))
            //  SUCESSO (vinculo entre cenário e tópico é valido): Executa o método que materializa os "mocks" dos serviços relacionados
            {
                _certificationService.Define(cenario);
                context.HttpContext.Response.Headers.Add(HEADER_CERTIFICACAO, new[] { cenario.ToString() });
            }
            //  FALHA (vínculo entre cenário e tópico inválido): finaliza a requisição com erro
            else
                context.Result = new JsonResult(new { Error = $"Header {HEADER_CERTIFICACAO} missing or unknown" }) { StatusCode = StatusCodes.Status409Conflict };
        }
        public void OnResourceExecuted(ResourceExecutedContext context) { }
        static bool Cenario_x_Metodo_Estao_Relacionados(ResourceExecutingContext context, CenariosCertificacaoEnum cenario) => ((ControllerActionDescriptor)context.ActionDescriptor)
                .MethodInfo
                .GetCustomAttributes(typeof(CenarioCertificacaoRelacionadoAttribute), false)?
                .FirstOrDefault(a => ((CenarioCertificacaoRelacionadoAttribute)a).CenarioCertificacao.Equals(cenario)) != null;
    }
}