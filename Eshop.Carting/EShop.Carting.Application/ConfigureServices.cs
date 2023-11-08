using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using EShop.Carting.Application.Common.Behaviours;
using Microsoft.Extensions.Configuration;
using EShop.Carting.Application.MessageHandlers;

namespace EShop.Carting.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<IItemServiceBusHandler, ItemServiceBusHandler>();

            services.Configure<ServiceBusHandlerOptions>(configuration.GetSection(ServiceBusHandlerOptions.Section));

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            
            var sp = services.BuildServiceProvider();
            var serviceBusHandler = sp.GetService<IItemServiceBusHandler>();
            serviceBusHandler?.Register().GetAwaiter().GetResult();

            return services;
        }
    }
}
