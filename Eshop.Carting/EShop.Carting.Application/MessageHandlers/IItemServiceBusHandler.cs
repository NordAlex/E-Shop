namespace EShop.Carting.Application.MessageHandlers
{
    public interface IItemServiceBusHandler
    {
        Task Register();
        Task CloseQueueAsync();
        ValueTask DisposeAsync();
    }
}
