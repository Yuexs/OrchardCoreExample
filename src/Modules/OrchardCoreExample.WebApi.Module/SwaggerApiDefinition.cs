using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using OrchardCore.Swagger.Abstractions;

namespace OrchardCoreExample.WebApi.Module
{
    public class SwaggerApiDefinition: ISwaggerApiDefinition
    {
        public string Name => "WebApiDemo";

        public OpenApiInfo OpenApiInfo => new OpenApiInfo()
        {
            Version = "v2",
            Title = "WebApi Demo",
            Description = "WebApiDemo",
            Contact = new OpenApiContact()
            {
                Name = "Yues.S",
                Email = "yuex.s@xd5u.com",
                Url = new Uri("https://www.xd5u.com")
            }
        };

        public Func<string, ApiDescription, bool> ApiDescriptionFilterPredicate => (name, description) =>
            description.RelativePath.StartsWith("api/demo");
    }
}
