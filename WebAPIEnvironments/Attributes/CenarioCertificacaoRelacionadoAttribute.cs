using System;
using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Attributes
{
    /// <summary>
    /// Atributo para vínculo de cenários de certificação e métodos expostos das APIs
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CenarioCertificacaoRelacionadoAttribute : Attribute
    {
        public CenariosCertificacaoEnum CenarioCertificacao { get; private set; }
        public CenarioCertificacaoRelacionadoAttribute(CenariosCertificacaoEnum cenarioCertificacao) => CenarioCertificacao = cenarioCertificacao;
    }
}