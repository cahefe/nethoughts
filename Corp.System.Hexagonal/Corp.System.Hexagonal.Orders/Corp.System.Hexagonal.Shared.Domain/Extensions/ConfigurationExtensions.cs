using Microsoft.Extensions.Configuration;

namespace Corp.System.Hexagonal.Shared.Domain.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfiguration InitConfiguration(string configFilename) => new ConfigurationBuilder()
                .AddJsonFile(configFilename)
                .Build();

        /// <summary>
        /// Obter uma entidade identificada a partir da seção no arquivo de configurações (appSettings.json)
        /// </summary>
        /// <typeparam name="TAppSettings">Nome da seção (no AppSettings)/entidade resultante</typeparam>
        /// <param name="configuration"></param>
        /// <returns>Entidade do tipo TAppSettings identificada a partir do arquivo de configuração</returns>
        /// <remarks>A seção deve ter o mesmo nome do tipo de dados retornado</remarks>
        public static TAppSettings GetAppSection<TAppSettings>(this IConfiguration configuration) => configuration.GetSection(typeof(TAppSettings).Name).Get<TAppSettings>();
    }
}
