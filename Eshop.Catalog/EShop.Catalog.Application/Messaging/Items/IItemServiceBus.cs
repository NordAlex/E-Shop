using EShop.Catalog.Domain.Entities;

namespace EShop.Catalog.Application.Messaging.Items
{
    public interface IItemServiceBus
    {
        public Task SendUpdatedItemAsync(Item updatedItem);
    }
}
