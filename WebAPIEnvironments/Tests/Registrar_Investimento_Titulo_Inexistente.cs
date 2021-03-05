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
        /// <summary>
        /// Gera a implementação da înterface para atender ao contexto exigido na execução do cenário
        /// </summary>
        /// <typeparam name="TInterface">Implementação mockada baseada na interface que se deseja implementar</typeparam>
        /// <returns>Instância mockada da interface</returns>
        /// <remarks>
        /// Atente-se que a partir do método é possível implementar todas as instâncias necessárias para atender ao cenário
        /// <remarks>
        public TInterface PrepareScenario<TInterface>()
        {
            if (typeof(TInterface).Equals(typeof(ICalculosService)))
            {
                Mock<ICalculosService> mock = new Mock<ICalculosService>();
                mock.Setup(m => m.CalcularSoma(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns<decimal, decimal>((v1, v2) => new ResultadoCalculo
                {
                    ContadorInstancias = _contadorInstâncias,
                    Abordagem = $"Conteúdo mockado ({v1:N2} + {v2:N2})",
                    Valor = (v1 + v2) * 1000,
                    Randomico = new Random().Next(1000),
                    Employee = _negociacaoContext.ObterPrimeiroEmployeeRandomico()
                });
                mock.Setup(m => m.GerarTexto(It.IsAny<int>())).Returns<int>(p1 => $"Aleatório mockado (instância: {_contadorInstâncias} - ciclos: {p1})");
                return (TInterface)mock.Object;
            }
            throw new NotImplementedException(nameof(TInterface));
        }
    }
}