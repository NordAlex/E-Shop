using Azure.Identity;
using Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Host.ConfigureAppConfiguration((context, config) =>
    {
        if (context.HostingEnvironment.IsDevelopment())
            return;

        var built = config.Build();
        var endpoint = built["ConfigurationManager:Endpoint"];
        if (string.IsNullOrWhiteSpace(endpoint))
            return;

        config.AddAzureAppConfiguration(o =>
            o.Connect(new Uri(endpoint), new DefaultAzureCredential())
             .Select("IdentityServer:*")
             .TrimKeyPrefix("IdentityServer:")
        );
    });

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}