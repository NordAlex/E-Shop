using AutoMapper;
using EShop.Catalog.Application.Common.Interfaces;
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

        public UpdateItemCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id, cancellationToken);
            if (category != null)
            {
                _mapper.Map(request, category);

                _context.Categories.Update(category);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
