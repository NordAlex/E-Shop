using AutoMapper;
using EShop.Carting.Application.CartItems.Queries.GetCartItems;
using EShop.Carting.Domain.Entities;

namespace EShop.Carting.Application.Common.Mapping
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<CartItem, CartItemDto>();
        }
    }
}
