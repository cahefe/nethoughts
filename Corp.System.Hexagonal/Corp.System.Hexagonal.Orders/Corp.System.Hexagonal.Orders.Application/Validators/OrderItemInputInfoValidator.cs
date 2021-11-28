using System;
using Corp.System.Hexagonal.Orders.Domain.Model;
using FluentValidation;

namespace Corp.System.Hexagonal.Orders.Application.Validators
{
    public class OrderItemInputInfoValidator : AbstractValidator<OrderItemBasetInfo>
    {
        public OrderItemInputInfoValidator()
        {
            RuleFor(model => (int)model.IDBond).GreaterThan(0);
            RuleFor(model => Math.Truncate(100 * model.Quantity * model.BondUnityPrice) / 100).GreaterThan(0m);
        }
    }
}
