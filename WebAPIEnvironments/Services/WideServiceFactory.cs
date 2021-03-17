using System;
using WebAPIEnvironments.Interfaces;

namespace WebAPIEnvironments.Services
{
    public class WideServiceFactory<TInterface> : IWideServiceFactory<TInterface>
    {
        protected readonly IServiceProvider _serviceProvider;
        public WideServiceFactory(IServiceProvider serviceProvider) => (_serviceProvider) = (serviceProvider);
        public virtual TInterface GetService(object context = null) => throw new NotImplementedException();
    }
}