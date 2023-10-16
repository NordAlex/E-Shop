using EShop.Catalog.Application.Common.Interfaces;
using EShop.Catalog.Infrastructure.Repositories;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Catalog.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LiteDbMemoryConnection");
            services.AddTransient<ILiteDatabase>(_ => new LiteDatabase(connectionString));
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            return services;
        }
    }
}
