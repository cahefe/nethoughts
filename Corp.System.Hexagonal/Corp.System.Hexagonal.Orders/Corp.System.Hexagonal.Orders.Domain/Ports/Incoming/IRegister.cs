using System.Threading.Tasks;
using Corp.System.Hexagonal.Orders.Domain.Model;

namespace Corp.System.Hexagonal.Orders.Domain.Ports.Incoming
{
    public interface IRegister
    {
        Task<OrderInfo> Execute(OrderBaseInfo orderBaseInfo);
    }
}
