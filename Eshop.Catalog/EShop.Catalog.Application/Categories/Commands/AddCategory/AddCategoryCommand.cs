using EShop.Catalog.Application.Common.Interfaces;
using EShop.Catalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.Application.Categories.Commands.AddCategory
{
    public record AddCategoryCommand : IRequest
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentCategoryId { get; set; }

    }

    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public AddCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Image = request.ImageUrl,
                Name = request.Name,
                ParentCategoryId = request.ParentCategoryId
            };

            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            var test= _context.Categories.ToList();
            ;
        }
    }
}
