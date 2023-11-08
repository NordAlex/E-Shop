namespace EShop.Carting.Application.MessageHandlers
{
    public class ServiceBusHandlerOptions
    {
        public const string Section = nameof(ServiceBusHandlerOptions);
        public string ConnectionString { get; set; }
        public string Topic { get; set; }
        public string Subscription { get; set; }
    }
}
