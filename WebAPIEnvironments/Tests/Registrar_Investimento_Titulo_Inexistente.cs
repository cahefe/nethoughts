using System;
using Moq;
using WebAPIEnvironments.Interfaces;
using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Tests
{
    /// <summary>
    /// Exemplo de implementação de classe de implementação de cenário de certificação, trazendo valores específicos para situações esperadas
    /// </summary>
    public class Registrar_Investimento_Titulo_Inexistente : ICertificationCenarios
    {
        static int _contadorInstâncias = 0;
        readonly DBContext_Tests_Negociacao _negociacaoContext;
        public Registrar_Investimento_Titulo_Inexistente(DBContext_Tests_Negociacao employeeContext)
        {
            ++_contadorInstâncias;
            _negociacaoContext = employeeContext;
            _negociacaoContext.IncluirEmployees(5);
        }
        public TInterface PrepareScenario<TInterface>()
        {
            if (typeof(TInterface).Equals(typeof(ICalculosService)))
            {
                Mock<ICalculosService> mock = new Mock<ICalculosService>();
                mock.Setup(m => m.CalcularSoma(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(new ResultadoCalculo
                {
                    ContadorInstancias = _contadorInstâncias,
                    Abordagem = "Conteúdo mockado",
                    Valor = 1,
                    Randomico = new Random().Next(1000),
                    Employee = _negociacaoContext.ObterPrimeiroEmployeeRandomico()
                });
                return (TInterface)mock.Object;
            }
            throw new NotImplementedException(nameof(TInterface));
        }
    }
}