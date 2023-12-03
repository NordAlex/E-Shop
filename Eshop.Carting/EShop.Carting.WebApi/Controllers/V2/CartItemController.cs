using Asp.Versioning;
using EShop.Carting.Application.CartItems.Queries.GetCartItems;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Carting.WebApi.Controllers.V2
{
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/cart")]
    public class CartItemController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CartItemDto>))]
        public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync([FromQuery] GetCartItemsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
