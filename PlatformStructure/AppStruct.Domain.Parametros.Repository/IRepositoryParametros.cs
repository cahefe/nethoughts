using AppStruct.Domain.Parametros.Model;

namespace AppStruct.Domain.Parametros.Repository
{
    public interface IRepositoryParametros
    {
        Parametro Obter(EnumParametro parametro);
        void Salvar(EnumParametro parametro, object valor);
        Endpoint Obter(long id);
        void Salvar(Endpoint endpoint);
    }
}
