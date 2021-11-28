namespace Corp.System.Hexagonal.Orders.Domain.Model
{
    public class OrderItemInfo
    {
        public short IDBond { get; set; }
        public decimal Quantity { get; set; }
        public decimal BondUnityPrice { get; set; }
        public decimal GrossValue { get; set; }
        public FeesInfo BrokerFees { get; set; }
        public FeesInfo CompanyFees { get; set; }
        public decimal NetValue { get; set; }
    }
}
