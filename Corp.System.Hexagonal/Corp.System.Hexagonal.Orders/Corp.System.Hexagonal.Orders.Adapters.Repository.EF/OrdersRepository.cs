using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Entity;
using Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Mappings;
using Corp.System.Hexagonal.Orders.Domain.Model;
using Corp.System.Hexagonal.Orders.Domain.Ports.Outgoing;

namespace Corp.System.Hexagonal.Orders.Adapters.Repository.EF
{
    public class OrdersRepository : IOrdersRepository
    {
        readonly OrdersContext _orderContext;
        readonly IMapper _map;

        public OrdersRepository(OrdersContext orderContext)
        {
            _orderContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile<OrdersProfile>());
            mapConfig.AssertConfigurationIsValid();
            _map = mapConfig.CreateMapper();
        }
        public async Task<OrderInfo> Create(OrderInfo orderInfo)
        {
            var order = _map.Map<Order>(orderInfo);
            _orderContext.Add(order);
            await _orderContext.SaveChangesAsync();
            return _map.Map<OrderInfo>(order);
        }

        public Task<OrderInfo> Delete(long IDOrder)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderInfo>> Read(OrderFilterInfo orderFilterInput)
        {
            // return _map.Map<OrderInfo[]>(_orderContext.Orders.ToList());
            throw new NotImplementedException();
        }

        public Task<OrderInfo> Update(OrderInfo orderInfo)
        {
            throw new NotImplementedException();
        }
    }
}
