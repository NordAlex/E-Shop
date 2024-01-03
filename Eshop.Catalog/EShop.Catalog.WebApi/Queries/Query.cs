using AutoMapper;
using AutoMapper.QueryableExtensions;
using EShop.Catalog.Application.Categories.Queries.GetCategories;
using EShop.Catalog.Application.Categories.Queries.GetCategory;
using EShop.Catalog.Application.Items.Queries.GetItems;
using EShop.Catalog.Infrastructure.Persistence;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.WebApi.Queries
{
    [Authorize(Roles = new string[] { "Buyer", "Manager" })]
    public class Query
    {
        private readonly IMapper _mapper;

        public Query(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<CategoryItemDto>> GetCategories([Service]ApplicationDbContext context, int pageNumber = 0, int pageSize = 0) 
            => await context.Categories.ProjectTo<CategoryItemDto>(_mapper.ConfigurationProvider).Skip(pageNumber).Take(pageSize).ToListAsync();
        
        public async Task<List<ItemDto>> GetItems([Service] ApplicationDbContext context, int pageNumber = 0, int pageSize = 0) 
            => await context.Items.ProjectTo<ItemDto>(_mapper.ConfigurationProvider).Skip(pageNumber).Take(pageSize).ToListAsync();
        
        public async Task<CategoryDetailsDto> GetCategory(int id, [Service] ApplicationDbContext context)
        {
            var category = await context.Categories.FindAsync(id);
            return _mapper.Map<CategoryDetailsDto>(category);
        }
        
        public async Task<ItemDto> GetItem(int id, [Service] ApplicationDbContext context)
        {
            var category = await context.Items.FindAsync(id);
            return _mapper.Map<ItemDto>(category);
        }
    }
}
