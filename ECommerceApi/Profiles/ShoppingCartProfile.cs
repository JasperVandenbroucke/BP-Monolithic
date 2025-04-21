using AutoMapper;
using ECommerceApi.Models.Dtos.ShoppingCarts;
using ECommerceApi.Models;

namespace ECommerceApi.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartReadDto>();
            CreateMap<ShoppingCartItem, ShoppingCartItemReadDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
            CreateMap<ShoppingCartItemCreateDto, ShoppingCartItem>()
                .ForMember(dest => dest.ShoppingCartId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
