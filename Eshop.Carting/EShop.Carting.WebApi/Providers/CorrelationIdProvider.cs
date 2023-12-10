namespace EShop.Carting.WebApi.Provider
{
    public class CorrelationIdImp : ICorrelationIdProvider
    {
        private string _correlationId = Guid.NewGuid().ToString();

        public string Get() => _correlationId;

        public void Set(string correlationId)
        {
            if (!string.IsNullOrWhiteSpace(correlationId))
            {
                _correlationId = correlationId;
            }
        }
    }
}
