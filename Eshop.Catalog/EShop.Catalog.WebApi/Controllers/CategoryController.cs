using Asp.Versioning;
using EShop.Catalog.Application.Categories.Commands.AddCategory;
using EShop.Catalog.Application.Categories.Commands.DeleteCategory;
using EShop.Catalog.Application.Categories.Commands.UpdateCategory;
using EShop.Catalog.Application.Categories.Queries.GetCategories;
using EShop.Catalog.Application.Categories.Queries.GetCategory;
using EShop.Catalog.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Catalog.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Buyer")]
    public class CategoryController : ApiControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddCategoryAsync(AddCategoryCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCategoryAsync([FromQuery]DeleteCategoryCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateCategoryAsync(UpdateCategoryCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsDto))]
        public async Task<ActionResult<CategoryDetailsDto>> GetCategoryAsync([FromQuery]GetCategoryDetailQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("/api/v{version:apiVersion}/categories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryItemDto>))]
        public async Task<ActionResult<PaginatedList<CategoryItemDto>>> GetCategoriesAsync([FromQuery]GetCategoriesQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
