using AutoMapper;
using EShop.Catalog.Application.Common.Interfaces;
using EShop.Catalog.Domain.Entities;
using MediatR;

namespace EShop.Catalog.Application.Items.Commands.DeleteItem
{
    public record DeleteItemCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteItemCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Items.FindAsync(request.Id, cancellationToken);
            if (category != null)
            {
                _context.Items.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
