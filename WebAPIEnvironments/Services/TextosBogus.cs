using WebAPIEnvironments.Interfaces;
using Bogus;

namespace WebAPIEnvironments.Services
{
    class TextosBogus : ITextos
    {
        public string Classe { get => "TextosBogus"; }

        public string GerarTexto(int digitos) => Classe + ":" + new Faker("pt_BR").Random.String(digitos);
    }
}