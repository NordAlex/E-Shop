using EShop.Carting.Application.CartItems.Commands.AddCartItem;
using EShop.Carting.Application.Common.Interfaces;
using EShop.Carting.Domain.Entities;
using MediatR;

namespace EShop.Carting.Application.CartItems.Commands.RemoveCartItem
{
    public record RemoveCartItemCommand : IRequest
    {
        public int CartId { get; set; }
        public int ItemId { get; set; }
    }

    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public RemoveCartItemCommandHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }


        public Task Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var entities = _cartItemRepository.GetCartItems(request.CartId);
            var entity = entities.FirstOrDefault(x => x.ItemId == request.ItemId);
            if (entity == null)
            {
                return Task.CompletedTask;
            }

            if (entity.Quantity == 1)
            {
                _cartItemRepository.Delete(entity);
            }
            else
            {
                entity.Quantity -= 1;
                _cartItemRepository.Update(entity);
            }

            return Task.CompletedTask;
        }
    }
}
