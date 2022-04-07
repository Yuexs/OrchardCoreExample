using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using OrchardCore.Swagger.Abstractions;

namespace OrchardCoreExample.AliYunSecurityTokenService.Module
{
    public class ApiDefinition : ISwaggerApiDefinition
    {
        public string Name => "AliYunSecurityTokenServiceAPI v1";

        public OpenApiInfo OpenApiInfo => new OpenApiInfo()
        {
            Version = "v1",
            Title = "AliYunSecurityTokenService",
            Description = "阿里云STS接口",
            Contact = new OpenApiContact()
            {
                Name = "Yues.S",
                Email = "yuex.s@xd5u.com",
                Url = new Uri("https://www.xd5u.com")
            }
        };

        public Func<string, ApiDescription, bool> ApiDescriptionFilterPredicate => (name, description) =>
            description.RelativePath.StartsWith("api/aliyunsecuritytokenservice/v1");
    }
}
