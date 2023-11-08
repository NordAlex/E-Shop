using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using EShop.Catalog.Application.Messaging.Base;
using EShop.Catalog.Domain.Entities;
using Microsoft.Extensions.Options;

namespace EShop.Catalog.Application.Messaging.Items
{
    public class ItemServiceBus : BaseServiceBus, IItemServiceBus
    {
        public ItemServiceBus(IOptions<ItemServiceBusOptions> options) : base(options)
        {
        }

        public async Task SendUpdatedItemAsync(Item updatedItem)
        {
            var messageBody = JsonSerializer.Serialize(updatedItem);

            //Set content type and Guid
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody))
            {
                MessageId = Guid.NewGuid().ToString(),
                ContentType = "application/json"
            };

            await Sender.SendMessageAsync(message);
        }
    }
}
