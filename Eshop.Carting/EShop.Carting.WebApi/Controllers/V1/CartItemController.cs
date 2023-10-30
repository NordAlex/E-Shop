using Asp.Versioning;
using EShop.Carting.Application.CartItems.Commands.AddCartItem;
using EShop.Carting.Application.CartItems.Commands.RemoveCartItem;
using EShop.Carting.Application.CartItems.Queries.GetCartInfo;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Carting.WebApi.Controllers.V1
{
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    [Route("/api/v{version:apiVersion}/cart")]
    public class CartItemController : ApiControllerBase
    {
        [HttpGet]
        [MapToApiVersion(1.0)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartInfoDto))]
        public async Task<CartInfoDto> GetCartItemsAsync([FromQuery] GetCartInfoQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCartItemAsync([FromBody] RemoveCartItemCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddCartItemAsync([FromBody] AddCartItemCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
