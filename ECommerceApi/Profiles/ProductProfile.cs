using AutoMapper;
using ECommerceApi.Models;
using ECommerceApi.Models.Dtos.Products;

namespace ECommerceApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Source -> Target
            CreateMap<Product, ProductReadDto>();
        }
    }
}
