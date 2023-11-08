using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace EShop.Catalog.Application.Messaging.Base
{
    public abstract class BaseServiceBus
    {
        protected readonly ServiceBusSender Sender;

        protected BaseServiceBus(IOptions<ServiceBusOptions> options)
        {
            var client = new ServiceBusClient(
                options.Value.ConnectionString,
                new DefaultAzureCredential());
            Sender = client.CreateSender(options.Value.Topic);
        }
    }
}
