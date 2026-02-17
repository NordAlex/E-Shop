using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AutoMapper;
using EShop.Carting.Application;
using EShop.Carting.Application.Common.Mapping;
using EShop.Carting.Infrastructure;
using EShop.Carting.WebApi.Filters;
using EShop.Carting.WebApi.Middleware;
using EShop.Carting.WebApi.Provider;
using EShop.Carting.WebApi.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<CorrelationIdFilter>();
});

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

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
            .Select("Carting:*")
            .TrimKeyPrefix("Carting:")
    );
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["Authority:Endpoint"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            NameClaimType = "name", //rename name claim for easier access
            RoleClaimType = "role", //rename role claim for easier access
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddScoped<ICorrelationIdProvider, CorrelationIdImp>();

builder.Services
    .AddApiVersioning()
    .AddApiExplorer(options =>
    {
        // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
        // note: the specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
        // can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;

        options.DefaultApiVersion = new ApiVersion(1, 0);

        //indicating whether a default version is assumed when a client does
        // does not provide an API version.
        options.AssumeDefaultVersionWhenUnspecified = true;
    });

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new DtoMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();

if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
