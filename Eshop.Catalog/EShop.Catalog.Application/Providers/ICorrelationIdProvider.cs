namespace EShop.Catalog.Application.Providers
{
    public interface ICorrelationIdProvider
    {
        string Get();
        void Set(string correlationId);
    }
}
