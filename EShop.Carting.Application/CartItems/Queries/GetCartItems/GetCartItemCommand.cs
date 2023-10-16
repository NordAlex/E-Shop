using EShop.Carting.Application.Common.Interfaces;
using MediatR;
using AutoMapper;

namespace EShop.Carting.Application.CartItems.Queries.GetCartItems
{
    public record GetCartItemsQuery : IRequest<List<CartItemDto>>
    {
        public int CartId { get; set; }
    }

    public class GetCartItemCommand : IRequestHandler<GetCartItemsQuery, List<CartItemDto>>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public GetCartItemCommand(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
        }

        public Task<List<CartItemDto>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            var cartItems = _cartItemRepository.GetCartItems(request.CartId).Select(x => _mapper.Map<CartItemDto>(x)).ToList();
            return Task.FromResult(cartItems);
        }
    }
}
