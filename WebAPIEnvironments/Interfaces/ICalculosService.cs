using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Interfaces
{
    public interface ICalculosService
    {
        ResultadoCalculo CalcularSoma(decimal valor1, decimal valor2);
        string GerarTexto(int ciclos);
    }
}