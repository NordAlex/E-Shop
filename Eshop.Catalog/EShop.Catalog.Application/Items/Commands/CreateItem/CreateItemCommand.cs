using AutoMapper;
using EShop.Catalog.Application.Common.Interfaces;
using EShop.Catalog.Domain.Entities;
using MediatR;

namespace EShop.Catalog.Application.Items.Commands.CreateItem
{
    public record CreateItemCommand : IRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public int Amount { get; set; }
    }

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateItemCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Item>(request);
            await _context.Items.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
