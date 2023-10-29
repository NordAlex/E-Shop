using AutoMapper;
using AutoMapper.QueryableExtensions;
using EShop.Catalog.Application.Common.Interfaces;
using EShop.Catalog.Application.Common.Mapping;
using EShop.Catalog.Application.Common.Models;
using MediatR;

namespace EShop.Catalog.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : IRequest<PaginatedList<CategoryItemDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 30;
    }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, PaginatedList<CategoryItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CategoryItemDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .ProjectTo<CategoryItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return categories;
        }
    }
}
