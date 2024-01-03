using EShop.Catalog.Application.Categories.Commands.AddCategory;
using EShop.Catalog.Application.Categories.Commands.DeleteCategory;
using EShop.Catalog.Application.Categories.Commands.UpdateCategory;
using EShop.Catalog.Application.Items.Commands.CreateItem;
using EShop.Catalog.Application.Items.Commands.DeleteItem;
using EShop.Catalog.Application.Items.Commands.UpdateItem;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Catalog.WebApi.Mutations
{
    public class Mutation
    {
        [Authorize(Roles = new [] { "Manager" })]
        public async Task<bool> AddCategoryAsync(AddCategoryCommand command, [FromServices] ISender mediator)
        {
            await mediator.Send(command);
            return true;
        }

        [Authorize(Roles = new [] { "Manager" })]
        public async Task<bool> DeleteCategoryAsync(DeleteCategoryCommand command, [FromServices] ISender mediator)
        {
            await mediator.Send(command);
            return true;
        }

        [Authorize(Roles = new [] { "Manager" })]
        public async Task<bool> UpdateCategoryAsync(UpdateCategoryCommand command, [FromServices] ISender mediator)
        {
            await mediator.Send(command);
            return true;
        }

        [Authorize(Roles = new [] { "Manager" })]
        public async Task<bool> AddItemAsync(CreateItemCommand command, [FromServices] ISender mediator)
        {
            await mediator.Send(command);
            return true;
        }

        [Authorize(Roles = new [] { "Manager" })]
        public async Task<bool> DeleteItemAsync([FromQuery] DeleteItemCommand command, [FromServices] ISender mediator)
        {
            await mediator.Send(command);
            return true;
        }

        [Authorize(Roles = new [] { "Manager" })]
        public async Task<bool> UpdateItemAsync(UpdateItemCommand command, [FromServices] ISender mediator)
        {
            await mediator.Send(command);
            return true;
        }
    }
}
