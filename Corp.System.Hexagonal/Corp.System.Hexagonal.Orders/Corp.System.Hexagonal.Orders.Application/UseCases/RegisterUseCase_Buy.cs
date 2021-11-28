using AutoMapper;
using Corp.System.Hexagonal.Orders.Domain.Model;
using Corp.System.Hexagonal.Orders.Domain.Ports.Outgoing;
using Corp.System.Hexagonal.Shared.Domain.Interfaces;
using FluentValidation;

namespace Corp.System.Hexagonal.Orders.Application.UseCases
{
    public class RegisterUseCase_Buy : RegisterUseCase
    {
        public override object ServiceLocatorContext { get; } = EnumOrderType.Buy;
        public RegisterUseCase_Buy(IOrdersRepository orderRepository, IServiceLocator<AbstractValidator<OrderBaseInfo>> validatorServiceLocator, IMapper map) : base(orderRepository, validatorServiceLocator, map) { }
    }
}
