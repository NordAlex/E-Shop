using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EShop.Carting.WebApi.Filters
{
    public class CorrelationIdFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = "x-correlation-id",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "uuid",
                        Description = "An unique identifier of request from submitter.",
                        Default = new OpenApiString(Guid.NewGuid().ToString())
                    },
                    Required = true // set to false if this is optional
                });
        }
    }
}
