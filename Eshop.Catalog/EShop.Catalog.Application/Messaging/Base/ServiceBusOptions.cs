namespace EShop.Catalog.Application.Messaging.Base
{
    public abstract class ServiceBusOptions
    {
        public string ConnectionString { get; set; }
        public string Topic { get; set; }
    }
}
