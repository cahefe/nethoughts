using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Interfaces
{
    public interface ICertificationTestService
    {
        CenariosCertificacaoEnum Cenario { get; }
        void Define(CenariosCertificacaoEnum cenario);
    }
}