using AutoMapper;
using EShop.Catalog.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : IRequest<List<CategoryItemDto>>
    {
        public int[] Id { get; set; }
    }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryItemDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.Where(x => request.Id.Contains(x.Id))
                .ToListAsync(cancellationToken: cancellationToken);
            return categories.Select(x => _mapper.Map<CategoryItemDto>(x)).ToList();
        }
    }
}
