using System;
using System.Runtime.Serialization;

namespace AppStruct.Domain.Common.Models.Notificacao
{
    public class Mensagem<TMensagem>
    {
        public readonly Type Tipo = typeof(TMensagem);
        public EnumTopico Topico { get; set; }
        public string Remetente { get; set; }
        public string Conteudo { get; set; }
        public readonly DateTime DataCriacao = DateTime.Now;
    }
}
