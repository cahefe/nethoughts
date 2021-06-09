using System;

namespace AppStruct.Domain.Parametros.Model
{
    public class Parametro
    {
        public EnumParametro ID { get; set; }
        public EnumTipoParametro TipoParametro { get; set; }
        public string Valor { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
