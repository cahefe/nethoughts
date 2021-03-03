using WebAPIEnvironments.Interfaces;
using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Services
{
    /// <summary>
    /// Identifica características do testes de certificação associado à requisição
    /// </summary>
    public class CertificationTestService : ICertificationTestService
    {
        public CenariosCertificacaoEnum Cenario { get; private set; } = CenariosCertificacaoEnum.Indefinido;
        public void Define(CenariosCertificacaoEnum cenario) => Cenario = cenario;
    }
}