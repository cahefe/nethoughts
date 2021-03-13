using System;
using WebAPIEnvironments.Interfaces;

namespace WebAPIEnvironments.Services
{
    class TextosFixos : ITextos
    {
        public string Classe { get => "TextosFixos"; }

        public string GerarTexto(int digitos) => Classe + ": " + new string('X', digitos);
    }
}