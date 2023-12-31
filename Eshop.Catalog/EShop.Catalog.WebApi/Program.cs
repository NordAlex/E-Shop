using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AutoMapper;
using EShop.Catalog.Application;
using EShop.Catalog.Application.Common.Mapping;
using EShop.Catalog.Application.Providers;
using EShop.Catalog.Infrastructure;
using EShop.Catalog.Infrastructure.Persistence;
using EShop.Catalog.WebApi.Filters;
using EShop.Catalog.WebApi.Middleware;
using EShop.Catalog.WebApi.Mutations;
using EShop.Catalog.WebApi.Provider;
using EShop.Catalog.WebApi.Queries;
using EShop.Catalog.WebApi.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<CorrelationIdFilter>();
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001/";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            NameClaimType = "name", //rename name claim for easier access
            RoleClaimType = "role", //rename role claim for easier access
        };
    });

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

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

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new DtoMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .RegisterDbContext<ApplicationDbContext>()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>();

var app = builder.Build();

app.MapGraphQL();

app.UseMiddleware<CorrelationIdMiddleware>();

// Configure the HTTP request pipeline.
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
