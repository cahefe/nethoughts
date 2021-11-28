using AutoMapper;
using Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Entity;
using Corp.System.Hexagonal.Orders.Domain.Model;

namespace Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Mappings
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<OrderItemInfo, OrderItem>()
            .ForMember(dest => dest.IDOrder, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.BrokerRate, opt => opt.MapFrom(src => src.BrokerFees.Rate))
            .ForMember(dest => dest.BrokerFee, opt => opt.MapFrom(src => src.BrokerFees.Value))
            .ForMember(dest => dest.CompanyRate, opt => opt.MapFrom(src => src.CompanyFees.Rate))
            .ForMember(dest => dest.CompanyFee, opt => opt.MapFrom(src => src.CompanyFees.Value))
            .ReverseMap();

            CreateMap<OrderInfo, Order>()
            .ForMember(dest => dest.MarkRegister, opt => opt.MapFrom(src => src.Marks.Register))
            .ForMember(dest => dest.MarkApproval, opt => opt.MapFrom(src => src.Marks.Approval))
            .ForMember(dest => dest.MarkReproval, opt => opt.MapFrom(src => src.Marks.Reproval))
            .ForMember(dest => dest.MarkCancel, opt => opt.MapFrom(src => src.Marks.Cancel))
            .ForMember(dest => dest.MarkSettle, opt => opt.MapFrom(src => src.Marks.Settle))
            .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => (byte)src.OrderType))
            .ForMember(dest => dest.OrderSituation, opt => opt.MapFrom(src => (byte)src.OrderSituation))
            .ForMember(dest => dest.IDClientParty, opt => opt.MapFrom(src => src.ClientParty.IDClient))
            .ForMember(dest => dest.IDClientCounterParty, opt => opt.MapFrom(src => src.ClientCounterParty.IDClient))
            .ReverseMap();

        }
    }
}
