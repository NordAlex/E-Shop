using EShop.Catalog.Application.Common.Interfaces;
using MediatR;

namespace EShop.Catalog.Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id, cancellationToken);
            if (category != null)
            {
                category.Name = request.Name;
                category.Image = request.ImageUrl;
                category.ParentCategoryId = request.CategoryId;

                _context.Categories.Update(category);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
