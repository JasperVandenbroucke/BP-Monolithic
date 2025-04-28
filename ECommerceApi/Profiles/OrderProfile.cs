using AutoMapper;
using ECommerceApi.Models;
using ECommerceApi.Models.Dtos.Orders;
using ECommerceApi.Models.Dtos.ShoppingCarts;

namespace ECommerceApi.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderItem, OrderItemReadDto>();
            CreateMap<ShoppingCartItemReadDto, OrderItem>();
            CreateMap<ShoppingCartReadDto, Order>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
