﻿using EShop.Catalog.Application.Common.Interfaces;
using EShop.Catalog.Domain.Entities;
using MediatR;

namespace EShop.Catalog.Application.CartItems.Commands.AddCartItem
{
    public record AddCartItemCommand : IRequest
    {
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }

    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public AddCartItemCommandHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }
        
        public Task Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var entities = _cartItemRepository.GetCartItems(request.CartId);
            var entity = entities.FirstOrDefault(x => x.ItemId == request.ItemId);
            if (entity == null)
            {
                var newEntity = new CartItem
                {
                    CartId = request.CartId,
                    ItemId = request.ItemId,
                    Name = request.Name,
                    ImageUrl = request.ImageUrl,
                    Price = request.Price,
                    Quantity = 1
                };

                _cartItemRepository.Insert(newEntity);
                return Task.CompletedTask;
            }

            entity.Quantity += 1;
            _cartItemRepository.Update(entity);
            return Task.CompletedTask;
        }
    }
}
