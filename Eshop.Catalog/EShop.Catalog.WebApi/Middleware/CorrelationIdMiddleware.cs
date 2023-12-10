
using EShop.Catalog.Application.Providers;
using Microsoft.ApplicationInsights.DataContracts;

namespace EShop.Catalog.WebApi.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrelationIdMiddleware" /> class.
        /// </summary>
        /// <param name="next">A request delegate that process an HTTP request.</param>
        public CorrelationIdMiddleware(
            RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// An implementation of invocation context for handling the request delegate.
        /// </summary>
        /// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>
        /// An empty result as a stub for required implementation pattern.
        /// </returns>
        public async Task InvokeAsync(HttpContext context, ICorrelationIdProvider correlationIdProvider)
        {
            if (context.Request.Headers.TryGetValue("x-correlation-id", out var correlationId))
            {
                correlationIdProvider.Set(correlationId);

                var feature = context.Features.Get<RequestTelemetry>();
                if (feature is not null)
                {
                    feature.Properties.TryAdd("Correlation ID", correlationId.ToString());
                }

                context.Response.Headers.Add("x-correlation-id", correlationId);
            }

            await _next(context);
        }
    }
}
