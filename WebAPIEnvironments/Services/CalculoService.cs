using System;
using WebAPIEnvironments.Interfaces;
using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Services
{
    public class CalculosService : ICalculosService
    {
        Random rnd = new Random();
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

        public string GerarTexto(int ciclos)
        {
            var result = "Texto concreto: ";
            for (int i = 0; i < ciclos; i++)
                result += rnd.Next(1000, 10000) + " ";
            return result.Trim();
        }
    }
}