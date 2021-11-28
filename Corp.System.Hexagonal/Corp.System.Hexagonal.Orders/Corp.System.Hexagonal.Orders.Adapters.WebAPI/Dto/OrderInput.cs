using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Corp.System.Hexagonal.Orders.Adapters.WebAPI.Dto
{
    public class OrderInput
    {
        [Required]
        public EnumOrderType OrderType { get; set; }
        public Client ClientParty { get; set; }
        public Client ClientCounterParty { get; set; }
        public IEnumerable<OrderItemInput> OrderItems { get; set; }
    }
}
