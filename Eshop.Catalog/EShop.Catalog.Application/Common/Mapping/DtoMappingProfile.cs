using AutoMapper;
using EShop.Catalog.Application.CartItems.Queries.GetCartItems;
using EShop.Catalog.Domain.Entities;

namespace EShop.Catalog.Application.Common.Mapping
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<CartItem, CartItemDto>();
        }
    }
}
