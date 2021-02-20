using System;

namespace WebAPIEnvironments
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class TopicoRelacionadoAttribute : Attribute
    {
        public TopicosCenariosCertificacaoEnum TopicoRelacionado { get; private set; }
        public TopicoRelacionadoAttribute(TopicosCenariosCertificacaoEnum topicoRelacionado) => TopicoRelacionado = topicoRelacionado;
    }
}