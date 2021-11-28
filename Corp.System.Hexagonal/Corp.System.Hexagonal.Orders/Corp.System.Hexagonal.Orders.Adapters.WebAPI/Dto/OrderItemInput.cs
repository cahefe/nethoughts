namespace Corp.System.Hexagonal.Orders.Adapters.WebAPI.Dto
{
    public class OrderItemInput
    {
        public short IDBond { get; set; }
        public decimal Quantity { get; set; }
        public decimal BondUnityPrice { get; set; }
    }
}
