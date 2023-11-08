using AutoMapper;
using EShop.Catalog.Application.Common.Interfaces;
using EShop.Catalog.Application.Messaging.Items;
using MediatR;

namespace EShop.Catalog.Application.Items.Commands.UpdateItem
{
    public record UpdateItemCommand : IRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public int Amount { get; set; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IItemServiceBus _itemServiceBus;

        public UpdateItemCommandHandler(IApplicationDbContext context, IMapper mapper, IItemServiceBus itemServiceBus)
        {
            _itemServiceBus = itemServiceBus;
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Id, cancellationToken);
            if (item != null)
            {
                _mapper.Map(request, item);

                _context.Items.Update(item);

                await _context.SaveChangesAsync(cancellationToken);

                await _itemServiceBus.SendUpdatedItemAsync(item);
            }
        }
    }
}
