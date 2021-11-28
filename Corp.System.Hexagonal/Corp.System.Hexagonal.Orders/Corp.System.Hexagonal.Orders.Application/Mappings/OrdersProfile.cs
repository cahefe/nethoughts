using AutoMapper;
using Corp.System.Hexagonal.Orders.Domain.Model;

namespace Corp.System.Hexagonal.Orders.Application.Mappings
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<OrderItemBasetInfo, OrderItemInfo>()
            .ForMember(dest => dest.GrossValue, opt => opt.Ignore())
            .ForMember(dest => dest.BrokerFees, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyFees, opt => opt.Ignore())
            .ForMember(dest => dest.NetValue, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<OrderBaseInfo, OrderInfo>()
            .ForMember(dest => dest.IDOrder, opt => opt.Ignore())
            .ForMember(dest => dest.OrderSituation, opt => opt.Ignore())
            .ForMember(dest => dest.ClientCounterParty, opt => opt.Ignore())
            .ForMember(dest => dest.Marks, opt => opt.MapFrom(s => MarksInfo.NewMarks()))
            .ForMember(dest => dest.TotalGrossValue, opt => opt.Ignore())
            .ForMember(dest => dest.TotalBrokerFees, opt => opt.Ignore())
            .ForMember(dest => dest.TotalCompanyFees, opt => opt.Ignore())
            .ForMember(dest => dest.TotalNetValue, opt => opt.Ignore())
            .ReverseMap();
        }
    }
}
