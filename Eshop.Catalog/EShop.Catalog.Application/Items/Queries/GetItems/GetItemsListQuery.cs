using AutoMapper;
using AutoMapper.QueryableExtensions;
using EShop.Catalog.Application.Common.Interfaces;
using EShop.Catalog.Application.Common.Mapping;
using EShop.Catalog.Application.Common.Models;
using MediatR;

namespace EShop.Catalog.Application.Items.Queries.GetItems
{
    public record GetItemsListQuery : IRequest<PaginatedList<ItemDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 30;
    }

    public class GetItemsListQueryHandler : IRequestHandler<GetItemsListQuery, PaginatedList<ItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ItemDto>> Handle(GetItemsListQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Items
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return items;
        }
    }
}
