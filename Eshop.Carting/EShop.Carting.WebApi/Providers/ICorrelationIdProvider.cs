namespace EShop.Carting.WebApi.Provider
{
    public interface ICorrelationIdProvider
    {
        string Get();
        void Set(string correlationId);
    }
}
