namespace AppStruct.Domain.Common.Service.DependencyInjection
{
    /// <summary>
    /// Padroniza o processo de seleção de implementações a partir de um contexto
    /// </summary>
    /// <typeparam name="TInterface">Tipo da interface a ser resolvida</typeparam>
    public interface IServiceSelector<TInterface>
    {
        /// <summary>
        /// Resolve a interface a partir de um contexto
        /// </summary>
        /// <param name="context">Contexto de seleção</param>
        /// <returns>Implementação a ser utilizada a partir da interface</returns>
        TInterface Select(object context);
    }
}
