using System;
using WebAPIEnvironments.Interfaces;

namespace WebAPIEnvironments.Services
{
    class WideServiceFactory<TInterface> : IWideServiceFactory<TInterface>
    {
        protected readonly IServiceProvider _serviceProvider;
        public WideServiceFactory(IServiceProvider serviceProvider) => (_serviceProvider) = (serviceProvider);
        public virtual TInterface GetService(object context) => throw new NotImplementedException();
    }
}