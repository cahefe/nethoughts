using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Corp.System.Hexagonal.Orders.Domain.Model;
using Corp.System.Hexagonal.Orders.Domain.Ports.Incoming;
using Corp.System.Hexagonal.Orders.Domain.Ports.Outgoing;
using Corp.System.Hexagonal.Shared.Domain.Interfaces;
using FluentValidation;

namespace Corp.System.Hexagonal.Orders.Application.UseCases
{
    public abstract class RegisterUseCase : IRegister, IServiceLocatorImplementation
    {
        public abstract object ServiceLocatorContext { get; }
        protected readonly IOrdersRepository _orderRepository;
        protected readonly AbstractValidator<OrderBaseInfo> _validator;
        protected readonly IMapper _map;
        public RegisterUseCase(IOrdersRepository orderRepository, IServiceLocator<AbstractValidator<OrderBaseInfo>> validatorServiceLocator, IMapper map)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _validator = (validatorServiceLocator ?? throw new ArgumentNullException(nameof(validatorServiceLocator))).GetService(this);
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public virtual async Task<OrderInfo> Execute(OrderBaseInfo orderBaseInfo)
        {
            var entryValidation = _validator.Validate(orderBaseInfo);
            if (!entryValidation.IsValid)
            {
                var msgError = "";
                foreach (var error in entryValidation.Errors)
                    msgError += $"{error.ErrorCode}. {error.PropertyName}: {error.ErrorMessage} > {error.CustomState}";
                throw new ArgumentException(msgError);
            }
            var orderInfo = _map.Map<OrderInfo>(orderBaseInfo);
            orderInfo.OrderSituation = EnumOrderSituation.Registered;
            orderInfo.TotalBrokerFees = orderInfo.OrderItems.Sum(oi => oi.BrokerFees?.Value ?? 0);
            orderInfo.TotalCompanyFees = orderInfo.OrderItems.Sum(oi => oi.CompanyFees?.Value ?? 0);
            orderInfo.TotalGrossValue = orderInfo.OrderItems.Sum(oi => oi.GrossValue);
            orderInfo.TotalNetValue = orderInfo.OrderItems.Sum(oi => oi.NetValue);
            return (OrderInfo)await _orderRepository.Create(orderInfo);
        }
    }
}
