using System;
using Corp.System.Hexagonal.Orders.Application.UseCases;
using Corp.System.Hexagonal.Orders.Domain.Model;
using Corp.System.Hexagonal.Orders.Domain.Ports.Incoming;
using Corp.System.Hexagonal.Shared.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Corp.System.Hexagonal.Orders.Application.ServiceLocators
{
    public class RegisterServiceLocator : IServiceLocator<IRegister>
    {
        readonly IServiceProvider _services;
        public RegisterServiceLocator(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }
        public IRegister GetService(object context) => (EnumOrderType)context switch
        {
            EnumOrderType.Buy => _services.GetService<RegisterUseCase_Buy>(),
            EnumOrderType.Sell => _services.GetService<RegisterUseCase_Sell>(),
            _ => throw new ArgumentOutOfRangeException(nameof(context), $"Not expected orderType value: {(EnumOrderType)context}"),
        };
    }
}
