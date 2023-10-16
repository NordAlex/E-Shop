using EShop.Catalog.Application.CartItems.Commands.AddCartItem;
using EShop.Catalog.Application.CartItems.Commands.RemoveCartItem;
using EShop.Catalog.Application.CartItems.Queries.GetCartItems;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Catalog.WebApi.Controllers
{
    public class CartItemController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync([FromQuery]GetCartItemsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCartItemAsync(RemoveCartItemCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddCartItemAsync(AddCartItemCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
