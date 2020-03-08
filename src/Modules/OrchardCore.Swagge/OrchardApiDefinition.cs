using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using OrchardCore.Swagger.Abstractions;

namespace OrchardCore.Swagger
{
    public class OrchardApiDefinition : ISwaggerApiDefinition
    {
        public string Name => "OrchardCoreAPI";

        public OpenApiInfo OpenApiInfo => new OpenApiInfo()
        {
            Version = "v2",
            Title = "OrchardCore API",
            Description = "An API to manage the OrchardCore installation",
            Contact =
                new OpenApiContact()
                {
                    Name = "Orchard Team",
                    Email = "info@orchardproject.net",
                    Url = new Uri("https://www.orchardproject.net")
                }
        };

        public Func<string, ApiDescription, bool> ApiDescriptionFilterPredicate => (name, description) =>
            description.RelativePath.Contains("api/tenants");

    }
}
