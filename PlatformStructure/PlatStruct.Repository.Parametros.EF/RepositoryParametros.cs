using System.Linq;
using AppStruct.Domain.Parametros.Model;
using AppStruct.Domain.Parametros.Repository;

namespace PlatStruct.Repository.Parametros.EF
{
    public class RepositoryParametros : ParametrosContext, IRepositoryParametros
    {
        public Parametro Obter(EnumParametro parametro) => Parametros.FirstOrDefault(p => p.ID.Equals(parametro));

        public Endpoint Obter(long id) => Endpoints.FirstOrDefault(e => e.ID.Equals(id));

        public void Salvar(EnumParametro parametro, object valor)
        {
            var found = Obter(parametro);
            if (found != null)
            {
                found.Valor = valor.ToString();
                SaveChanges();
            }
        }

        public void Salvar(Endpoint endpoint)
        {
            var found = Obter(endpoint.ID);
            if (found != null)
            {
                found.Fornecedor = endpoint.Fornecedor;
                found.port = endpoint.port;
                found.servidor = endpoint.servidor;
                found.url = endpoint.url;
            }
            else
                Endpoints.Add(endpoint);
            SaveChanges();
        }
    }
}
