using AutoMapper;
using EShop.Catalog.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.Application.Items.Queries.GetItems
{
    public record GetItemsListQuery : IRequest<List<ItemDto>>
    {
        public int[] Id { get; set; }
    }

    public class GetItemsListQueryHandler : IRequestHandler<GetItemsListQuery, List<ItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> Handle(GetItemsListQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Items.Where(x => request.Id.Contains(x.Id))
                .ToListAsync(cancellationToken: cancellationToken);
            return items.Select(x => _mapper.Map<ItemDto>(x)).ToList();
        }
    }
}
