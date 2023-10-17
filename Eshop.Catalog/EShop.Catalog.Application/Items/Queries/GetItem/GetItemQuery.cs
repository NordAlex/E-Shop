using AutoMapper;
using EShop.Catalog.Application.Common.Interfaces;
using MediatR;

namespace EShop.Catalog.Application.Items.Queries.GetItem
{
    public record GetItemQuery : IRequest<ItemDetailsDto>
    {
        public int[] Id { get; set; }
    }

    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemDetailsDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Id);

            return _mapper.Map<ItemDetailsDto>(item);
        }
    }
}
