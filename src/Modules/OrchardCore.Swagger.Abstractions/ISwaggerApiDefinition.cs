using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace OrchardCore.Swagger.Abstractions
{
    public interface ISwaggerApiDefinition
    {
        string Name { get; }

        OpenApiInfo OpenApiInfo { get; }

        Func<string, ApiDescription, bool> ApiDescriptionFilterPredicate { get; }
    }
}