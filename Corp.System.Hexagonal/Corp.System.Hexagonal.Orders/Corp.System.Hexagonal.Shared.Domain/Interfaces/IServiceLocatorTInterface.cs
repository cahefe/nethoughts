namespace Corp.System.Hexagonal.Shared.Domain.Interfaces
{
    /// <summary>
    /// Implements a contract to format service location
    /// </summary>
    /// <typeparam name="TInterface">Related service's interface</typeparam>
    public interface IServiceLocator<TInterface>
    {
        /// <summary>
        /// Get service related to a context
        /// </summary>
        /// <param name="context">Context of choice. Diferente for each interface</param>
        /// <returns></returns>
        TInterface GetService(object context);
    }
}
