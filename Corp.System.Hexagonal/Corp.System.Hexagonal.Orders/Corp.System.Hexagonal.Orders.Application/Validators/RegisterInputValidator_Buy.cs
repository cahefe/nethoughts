namespace Corp.System.Hexagonal.Orders.Application.Validators
{
    public class RegisterInputValidator_Buy : RegisterInputValidator
    {
        public RegisterInputValidator_Buy() : base()
        {
            RuleForEach(model => model.OrderItems).SetValidator(new OrderItemInputInfoValidator()); ;
        }
    }
}
