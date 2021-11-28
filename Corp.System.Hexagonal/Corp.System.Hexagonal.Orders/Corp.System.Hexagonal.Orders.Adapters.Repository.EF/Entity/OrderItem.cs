using System.ComponentModel.DataAnnotations;

namespace Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Entity
{
    public class OrderItem
    {
        [Key]
        public long IDOrder { get; set; }
        public virtual Order Order { get; set; }
        [Key]
        public short IDBond { get; set; }
        public decimal Quantity { get; set; }
        public decimal BondUnityPrice { get; set; }
        public decimal GrossValue { get; set; }
        public decimal BrokerRate { get; set; }
        public decimal BrokerFee { get; set; }
        public decimal CompanyRate { get; set; }
        public decimal CompanyFee { get; set; }
        public decimal NetValue { get; set; }
    }
}
