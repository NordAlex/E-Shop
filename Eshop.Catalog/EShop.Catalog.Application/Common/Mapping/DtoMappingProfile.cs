using AutoMapper;
using EShop.Catalog.Application.Categories.Queries.GetCategories;
using EShop.Catalog.Application.Categories.Queries.GetCategory;
using EShop.Catalog.Application.Items.Commands.CreateItem;
using EShop.Catalog.Application.Items.Queries.GetItems;
using EShop.Catalog.Domain.Entities;

namespace EShop.Catalog.Application.Common.Mapping
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Category, CategoryItemDto>();
            CreateMap<Category, CategoryDetailsDto>();
            CreateMap<CreateItemCommand, Item>();
            CreateMap<Item, ItemDto>();
        }
    }
}
