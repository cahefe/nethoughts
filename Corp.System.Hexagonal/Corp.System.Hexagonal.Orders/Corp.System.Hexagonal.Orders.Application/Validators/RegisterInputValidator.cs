using Corp.System.Hexagonal.Orders.Domain.Model;
using FluentValidation;

namespace Corp.System.Hexagonal.Orders.Application.Validators
{
    public abstract class RegisterInputValidator : AbstractValidator<OrderBaseInfo>
    {
        public RegisterInputValidator()
        {
            RuleFor(model => model.OrderType).NotEqual(EnumOrderType.Unknown);
            RuleFor(model => model.OrderItems).NotNull();
        }
    }
}
