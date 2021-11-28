using AutoMapper;
using Corp.System.Hexagonal.Orders.Adapters.WebAPI.Dto;
using Corp.System.Hexagonal.Orders.Domain.Model;

namespace Corp.System.Hexagonal.Orders.Adapters.WebAPI
{
    public class DtoXModelProfile : Profile
    {
        public DtoXModelProfile()
        {
            CreateMap<OrderItemInput, OrderItemBasetInfo>()
            .ReverseMap();

            CreateMap<Client, ClientInfo>()
            .ReverseMap();

            CreateMap<OrderInput, OrderBaseInfo>()
            .ReverseMap();
        }
    }
}
