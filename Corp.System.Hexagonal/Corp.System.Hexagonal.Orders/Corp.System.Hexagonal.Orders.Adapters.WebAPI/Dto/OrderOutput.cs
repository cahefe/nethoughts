using System.Collections.Generic;

namespace Corp.System.Hexagonal.Orders.Adapters.WebAPI.Dto
{
    public class OrderOutput
    {
        public long IDOrder { get; set; }
        public EnumOrderType OrderType { get; set; }
        public Client ClientParty { get; set; }
        public Client ClientCounterParty { get; set; }
        public IEnumerable<OrderItemInput> OrderItems { get; set; }
        public EnumOrderSituation OrderSituation { get; set; }
    }
}
