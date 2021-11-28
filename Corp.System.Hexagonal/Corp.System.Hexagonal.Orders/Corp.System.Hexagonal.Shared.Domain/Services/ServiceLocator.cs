using System;
using System.Linq;
using Corp.System.Hexagonal.Shared.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Corp.System.Hexagonal.Shared.Domain.Services
{
    public class ServiceLocator : IServiceLocator
    {
        readonly IServiceProvider _services;
        public ServiceLocator(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }
        public TInterface GetService<TInterface>(object context)
        {
            var services = _services.GetServices(typeof(TInterface));
            // var services = _services.GetServices<TInterface>();
            var implementation = services.FirstOrDefault(s => ((IServiceLocatorImplementation)s).ServiceLocatorContext.Equals(context));
            return (TInterface)implementation;
            // _services.GetServices<TInterface>().FirstOrDefault(i => ((IServiceLocatorImplementation)i).ServiceLocatorContext == context);
        }
    }
}
