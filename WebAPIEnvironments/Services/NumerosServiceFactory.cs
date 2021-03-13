using System;
using WebAPIEnvironments.Interfaces;

namespace WebAPIEnvironments.Services
{
    class NumerosServiceFactory : WideServiceFactory<INumeros>
    {
        public NumerosServiceFactory(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override INumeros GetService(object context) => (int)context % 2 == 0 ? (INumeros)_serviceProvider.GetService(typeof(NumerosPositivos)) : (INumeros)_serviceProvider.GetService(typeof(NumerosNegativos));
    }
}