using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("ocelot.json")
    .Build();
builder.Services.AddOcelot(configuration);

builder.Services.AddSwaggerForOcelot(configuration, (o) =>
{
    o.GenerateDocsForGatewayItSelf = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerForOcelotUI();
}


await app.UseOcelot();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
