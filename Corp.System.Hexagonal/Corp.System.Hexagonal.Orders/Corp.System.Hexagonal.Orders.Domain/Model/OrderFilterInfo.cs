using System;
using System.Collections.Generic;

namespace Corp.System.Hexagonal.Orders.Domain.Model
{
    public class OrderFilterInfo
    {
        public virtual long IDOrder { get; set; }
        public virtual IEnumerable<EnumOrderType> OrderType { get; set; }
        public virtual IEnumerable<EnumOrderSituation> OrderSituation { get; set; }
        public virtual long IDPartyBroker { get; set; }
        public virtual long IDCounterPartyBroker { get; set; }
        public virtual long IDPartyClient { get; set; }
        public virtual long IDCounterPartyClient { get; set; }
        public virtual long IDBond { get; set; }
        public DateTime? MarkRegister { get; set; }
    }
}
