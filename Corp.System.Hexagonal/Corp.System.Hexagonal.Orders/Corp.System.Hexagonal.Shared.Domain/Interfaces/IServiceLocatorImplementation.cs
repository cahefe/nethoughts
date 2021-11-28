namespace Corp.System.Hexagonal.Shared.Domain.Interfaces
{
    /// <summary>
    /// Allows Service Locator pattern to choose between Implementations
    /// </summary>
    public interface IServiceLocatorImplementation
    {
        object ServiceLocatorContext { get; }
    }
}
