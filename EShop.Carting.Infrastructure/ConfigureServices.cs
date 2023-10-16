using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using EShop.Carting.Application.Common.Interfaces;
using EShop.Carting.Infrastructure.Repositories;
using LiteDB;

namespace EShop.Carting.Infrastructure
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
