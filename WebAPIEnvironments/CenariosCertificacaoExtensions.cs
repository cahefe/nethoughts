using Microsoft.AspNetCore.Builder;

namespace WebAPIEnvironments
{
    public static class CenariosCertificacaoExtensions
    {
        public static void UseCenariosCertificacao(this IApplicationBuilder builder) => builder.UseMiddleware<CenariosCertificacao>();
    }
}
