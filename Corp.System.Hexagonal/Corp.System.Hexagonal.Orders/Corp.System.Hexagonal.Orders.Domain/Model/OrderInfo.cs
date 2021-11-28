using System.Collections.Generic;

namespace Corp.System.Hexagonal.Orders.Domain.Model
{
    public class OrderInfo
    {
        public long IDOrder { get; set; }
        public EnumOrderType OrderType { get; set; }
        public ClientInfo ClientParty { get; set; }
        public IEnumerable<OrderItemInfo> OrderItems { get; set; }
        public EnumOrderSituation OrderSituation { get; set; }
        public virtual ClientInfo ClientCounterParty { get; set; }
        public MarksInfo Marks { get; set; }
        public decimal TotalGrossValue { get; set; }
        public decimal TotalBrokerFees { get; set; }
        public decimal TotalCompanyFees { get; set; }
        public decimal TotalNetValue { get; set; }
    }
}
