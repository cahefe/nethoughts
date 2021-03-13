namespace WebAPIEnvironments.Interfaces
{
    /// <summary>
    /// Defines a standard way to access services that aims any type of choice
    /// </summary>
    /// <typeparam name="TInterface">Type of interface to be served</typeparam>
    public interface IWideServiceFactory<TInterface>
    {
        /// <summary>
        /// Resolves which implementation of an instance to serve, based on a given context
        /// </summary>
        /// <param name="context">Context to be analized</param>
        /// <returns>Implementation of an instance</returns>
        TInterface GetService(object context);
    }
}