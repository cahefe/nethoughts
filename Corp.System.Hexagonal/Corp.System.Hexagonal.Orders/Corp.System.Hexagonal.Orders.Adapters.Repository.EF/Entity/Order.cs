using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Entity
{
    public class Order
    {
        [Key]
        public long IDOrder { get; set; }
        public byte OrderType { get; set; }
        public byte OrderSituation { get; set; }
        public long IDClientParty { get; set; }
        public long? IDClientCounterParty { get; set; }
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
        public decimal TotalGrossValue { get; set; }
        public decimal TotalBrokerFees { get; set; }
        public decimal TotalCompanyFees { get; set; }
        public decimal TotalNetValue { get; set; }
        public DateTime MarkRegister { get; set; }
        public DateTime? MarkApproval { get; set; }
        public DateTime? MarkReproval { get; set; }
        public DateTime? MarkCancel { get; set; }
        public DateTime? MarkSettle { get; set; }
    }
}
