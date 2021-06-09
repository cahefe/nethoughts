using System.Collections.Generic;
using AppStruct.Domain.Parametros.Model;

namespace AppStruct.Domain.Parametros.Service
{
    public interface IParametrosService
    {
        Parametro Obter(EnumParametro parametro);
        object ObterValor(EnumParametro parametro);
        Parametro Salvar(EnumParametro parametro, object valor);
        IEnumerable<Parametro> Listar(EnumTipoParametro tipoParametro);
        Endpoint Obter(long id);
    }
}
