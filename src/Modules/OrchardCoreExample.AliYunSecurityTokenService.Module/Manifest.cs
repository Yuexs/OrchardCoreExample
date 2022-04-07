using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "OrchardCoreExample.AliYunSecurityTokenService.Module",
    Author = "Yuex.S",
    Website = "https://gitee.com/YuexS/OrchardCoreExample",
    Version = "1.0.0",
    Description = "AliYunSecurityTokenService",
    Category = "Example",
    Dependencies = new[]
    {
        "OrchardCore.Swagger.API",
        "OrchardCore.OpenId.Server",
        "OrchardCore.OpenId.Validation"
    }
)]