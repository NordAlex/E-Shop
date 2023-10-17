using AutoMapper;
using EShop.Catalog.Application.Common.Interfaces;
using MediatR;

namespace EShop.Catalog.Application.Categories.Queries.GetCategory
{
    public record GetCategoryDetailQuery : IRequest<CategoryDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, CategoryDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDetailsDto> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            return _mapper.Map<CategoryDetailsDto>(category);
        }
    }
}
