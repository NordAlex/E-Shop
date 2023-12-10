using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using EShop.Catalog.Application.Messaging.Base;
using EShop.Catalog.Application.Providers;
using EShop.Catalog.Domain.Entities;
using Microsoft.Extensions.Options;

namespace EShop.Catalog.Application.Messaging.Items
{
    public class ItemServiceBus : BaseServiceBus, IItemServiceBus
    {
        private readonly ICorrelationIdProvider _correlationIdProvider;
        public ItemServiceBus(IOptions<ItemServiceBusOptions> options, ICorrelationIdProvider correlationIdProvider) : base(options)
        {
            _correlationIdProvider = correlationIdProvider;
        }

        public async Task SendUpdatedItemAsync(Item updatedItem)
        {
            var messageBody = JsonSerializer.Serialize(updatedItem);

            //Set content type and Guid
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody))
            {
                MessageId = Guid.NewGuid().ToString(),
                CorrelationId = _correlationIdProvider.Get(),
                ContentType = "application/json"
            };
            message.ApplicationProperties.Add("x-correlation", _correlationIdProvider.Get());

            await Sender.SendMessageAsync(message);
        }
    }
}
