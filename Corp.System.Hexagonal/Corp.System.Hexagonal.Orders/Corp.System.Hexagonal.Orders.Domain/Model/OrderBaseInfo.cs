using System.Collections.Generic;

namespace Corp.System.Hexagonal.Orders.Domain.Model
{
    public class OrderBaseInfo
    {
        public EnumOrderType OrderType { get; set; }
        public ClientInfo ClientParty { get; set; }
        public IEnumerable<OrderItemBasetInfo> OrderItems { get; set; }
    }
}
