using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Catalog.WebApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [Authorize(Roles = "Buyer")]
    public class CategoryController : ApiControllerBase
    {
        [HttpGet("properties")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Dictionary<string, string>>> GetCategoryPropertiesAsync([FromQuery] int id)
        {
            var property = new Dictionary<string, string>()
            {
                { "Brand", "test" },
                { "ItemProp1", "test2" }
            };
            return Ok(property);
        }
    }
}
