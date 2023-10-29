using Asp.Versioning;
using EShop.Catalog.Application.Categories.Queries.GetCategories;
using EShop.Catalog.Application.Items.Commands.CreateItem;
using EShop.Catalog.Application.Items.Commands.DeleteItem;
using EShop.Catalog.Application.Items.Commands.UpdateItem;
using Microsoft.AspNetCore.Mvc;
using EShop.Catalog.Application.Common.Models;
using EShop.Catalog.Application.Items.Queries.GetItems;

namespace EShop.Catalog.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class ItemController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddItemAsync(CreateItemCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteItemAsync([FromQuery] DeleteItemCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateItemAsync(UpdateItemCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("/api/v{version:apiVersion}/[controller]s")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<ItemDto>>> GetItemsAsync([FromQuery] GetItemsListQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
