using Azure.Messaging.ServiceBus;
using Azure.Identity;
using Microsoft.Extensions.Options;
using MediatR;
using EShop.Carting.Application.CartItems.Commands.UpdateCartProperies;
using System.Text.Json;

namespace EShop.Carting.Application.MessageHandlers
{
    public class ItemServiceBusHandler : IItemServiceBusHandler
    {
        private readonly ServiceBusProcessor _serviceBusProcessor;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ISender _mediator;

        public ItemServiceBusHandler(IOptions<ServiceBusHandlerOptions> options, ISender mediator)
        {
            _mediator = mediator;

            var clientOptions = new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            _serviceBusClient = new ServiceBusClient(
                options.Value.ConnectionString,
                new DefaultAzureCredential(),
                clientOptions);

            // create a processor that we can use to process the messages
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(options.Value.Topic, options.Value.Subscription, new ServiceBusProcessorOptions());
        }

        public async Task Register()
        {
            // add handler to process messages
            _serviceBusProcessor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await _serviceBusProcessor.StartProcessingAsync();
        }

        public async Task CloseQueueAsync()
        {
            await _serviceBusProcessor.CloseAsync().ConfigureAwait(false);
        }

        public async ValueTask DisposeAsync()
        {
            if (_serviceBusProcessor != null)
            {
                _serviceBusProcessor.ProcessMessageAsync -= MessageHandler;
                _serviceBusProcessor.ProcessErrorAsync -= ErrorHandler;
                await _serviceBusProcessor.DisposeAsync().ConfigureAwait(false);
            }

            if (_serviceBusClient != null)
            {
                await _serviceBusClient.DisposeAsync().ConfigureAwait(false);
            }
        }

        // handle received messages
        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");

            var message = JsonSerializer.Deserialize<ItemMessage>(body);

            await _mediator.Send(new UpdateCartPropertiesCommand()
            {
                ImageUrl = message.Image,
                ItemId = message.Id,
                Name = message.Name,
                Price = message.Price
            }, args.CancellationToken);

            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        private static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
