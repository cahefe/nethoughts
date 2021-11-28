using System;
using Corp.System.Hexagonal.Orders.Application.UseCases;
using Corp.System.Hexagonal.Orders.Application.Validators;
using Corp.System.Hexagonal.Orders.Domain.Model;
using Corp.System.Hexagonal.Shared.Domain.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Corp.System.Hexagonal.Orders.Application.ServiceLocators
{
    public class RegisterInputServiceLocator : IServiceLocator<AbstractValidator<OrderBaseInfo>>
    {
        readonly IServiceProvider _services;
        public RegisterInputServiceLocator(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public AbstractValidator<OrderBaseInfo> GetService(object context)
        {
            if (context is RegisterUseCase_Buy)
                return _services.GetService<RegisterInputValidator_Buy>();
            if (context is RegisterUseCase_Sell)
                return null;
            throw new InvalidCastException(nameof(context) + ":" + context?.GetType().Name);
        }
    }
}
