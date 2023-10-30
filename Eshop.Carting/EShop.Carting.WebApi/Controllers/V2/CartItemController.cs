using Asp.Versioning;
using EShop.Carting.Application.CartItems.Queries.GetCartItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EShop.Carting.WebApi.Controllers.V2
{
    [ApiVersion(2.0)]
    public class CartItemController : ApiControllerBase
    {
        [HttpGet("/api/v{version:apiVersion}/cart")]
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
