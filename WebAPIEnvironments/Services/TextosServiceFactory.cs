using System;
using WebAPIEnvironments.Interfaces;

namespace WebAPIEnvironments.Services
{
    class TextosServiceFactory : WideServiceFactory<ITextos>
    {
        public TextosServiceFactory(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override ITextos GetService(object context)
        {
            switch ((char)context)
            {
                case 'F': return (ITextos)_serviceProvider.GetService(typeof(TextosFixos));
                default:
                    return (ITextos)_serviceProvider.GetService(typeof(TextosBogus));
            }
        }
    }
}