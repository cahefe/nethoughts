using System.Collections.Generic;
using System.Threading.Tasks;
using Corp.System.Hexagonal.Orders.Domain.Model;

namespace Corp.System.Hexagonal.Orders.Domain.Ports.Outgoing
{
    public interface IOrdersRepository
    {
        Task<OrderInfo> Create(OrderInfo orderInfo);
        Task<IEnumerable<OrderInfo>> Read(OrderFilterInfo orderFilterInput);
        Task<OrderInfo> Update(OrderInfo orderInfo);
        Task<OrderInfo> Delete(long IDOrder);
    }
}
