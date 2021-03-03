using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using WebAPIEnvironments.Abstracts;
using WebAPIEnvironments.Models;
using WebAPIEnvironments.Repositories;

namespace WebAPIEnvironments.Tests
{
    public class DBContext_Tests_Negociacao : DBContext_Tests_InMemory<NegociacaoDBContext, DBContext_Tests_Negociacao>
    {
        Random _random;
        public DBContext_Tests_Negociacao(ILogger<DBContext_Tests_Negociacao> logger) : base(logger)
        {
            _random = new Random();
        }
        public void IncluirEmployees(int quantidade)
        {
            for (int cont = 0; cont < quantidade; cont++)
                Context.Employees.Add(new Employee
                {
                    Age = _random.Next(18, 69),
                    FirstName = "Nome",
                    Id = cont + 1,
                    LastName = $"Sobrenome {cont + 1}"
                });
            Context.SaveChanges();
            _logger.LogInformation($"*** Criados {Context.Employees.Count()} empregados");
        }
        public Employee ObterPrimeiroEmployeeRandomico()
        {
            var r = _random.Next(1, Context.Employees.Count());
            var e = Context.Employees.First(e => e.Id == r);
            _logger.LogInformation($"*** Empregado retornado - Id: {e.Id} + {e.FirstName} {e.LastName}");
            return e;
        }
    }
}