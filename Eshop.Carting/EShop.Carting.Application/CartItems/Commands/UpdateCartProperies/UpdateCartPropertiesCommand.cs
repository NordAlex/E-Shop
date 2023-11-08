using EShop.Carting.Application.Common.Interfaces;
using EShop.Carting.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EShop.Carting.Application.CartItems.Commands.UpdateCartProperies
{
    public record UpdateCartPropertiesCommand : IRequest
    {
        [Required]
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateCartPropertiesCommandHandler : IRequestHandler<UpdateCartPropertiesCommand>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public UpdateCartPropertiesCommandHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public Task Handle(UpdateCartPropertiesCommand request, CancellationToken cancellationToken)
        {
            var entities = _cartItemRepository.GetItems(request.ItemId);

            foreach (var entity in entities)
            {
                entity.Name = request.Name;
                entity.Price = request.Price;
                entity.ImageUrl = request.ImageUrl;
            }

            _cartItemRepository.Update(entities);
            return Task.CompletedTask;
        }
    }
}
