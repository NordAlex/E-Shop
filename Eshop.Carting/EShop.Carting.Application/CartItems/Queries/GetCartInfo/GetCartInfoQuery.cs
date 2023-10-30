using AutoMapper;
using EShop.Carting.Application.Common.Interfaces;
using MediatR;

namespace EShop.Carting.Application.CartItems.Queries.GetCartInfo
{
    public record GetCartInfoQuery : IRequest<CartInfoDto>
    {
        public string CartId { get; set; }
    }

    public class GetCartInfoQueryHandler : IRequestHandler<GetCartInfoQuery, CartInfoDto>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public GetCartInfoQueryHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
        }

        public Task<CartInfoDto> Handle(GetCartInfoQuery request, CancellationToken cancellationToken)
        {
            var cartItems = _cartItemRepository.GetCartItems(request.CartId).Select(x => _mapper.Map<CartInfoItemDto>(x)).ToList();

            return Task.FromResult(new CartInfoDto
            {
                Id = request.CartId,
                Items = cartItems
            });
        }
    }
}
