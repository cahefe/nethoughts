namespace Corp.System.Hexagonal.Shared.Domain.Interfaces
{
    /// <summary>
    /// Implements a contract to format service location
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// Get service related to a context for a specific Interface
        /// </summary>
        /// <param name="context">Context of choice. Diferente for each interface</param>
        /// <typeparam name="TInterface">Related service's interface</typeparam>
        /// <returns>A instance of service that matches the interface and context</returns>
        TInterface GetService<TInterface>(object context);
    }
}
