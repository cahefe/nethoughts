using WebAPIEnvironments.Interfaces;
using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Services
{
    public class CalculosService : ICalculosService
    {
        public ResultadoCalculo CalcularSoma(decimal valor1, decimal valor2) => new ResultadoCalculo
        {
            Abordagem = "Implementação concreta",
            Valor = valor1 + valor2,
            ContadorInstancias = -1,
            Employee = new Employee {
                Id = 999999,
                Age = 100,
                FirstName = "Employee",
                LastName = "Padrão"
            }
        };
    }
}