using System;
using WebAPIEnvironments.Interfaces;
using WebAPIEnvironments.Tests;

namespace WebAPIEnvironments.Services
{
    /// <summary>
    /// Fornece uma engine padrão para resolver qual serviço de teste deve ser entregue
    /// </summary>
    /// <typeparam name="TInterface">Interface a ser mockada</typeparam>
    public class TestsServiceFactory<TInterface> : WideServiceFactory<TInterface>
    {
        readonly ICertificationTestService _certificationTestService;
        public TestsServiceFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _certificationTestService = (ICertificationTestService)serviceProvider.GetService(typeof(ICertificationTestService));
        }
        public override TInterface GetService(object context = null)
        {
            var objType = Type.GetType($"{typeof(TestsBaseType).Namespace}.{_certificationTestService.Cenario}");
            if (objType == null)
                throw new InvalidCastException($"Failed on trying to obtain {nameof(TInterface)}");
            return ((ICertificationCenarios)_serviceProvider.GetService(objType)).PrepareScenario<TInterface>();
        }
    }
}