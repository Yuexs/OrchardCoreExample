using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "WebApi以及授权模块",
    Author = "Yuex.S",
    Website = "https://gitee.com/YuexS/OrchardCoreExample",
    Version = "1.0.0",
    Description = "WebApi以及授权模块",
    Category = "Example",
    Dependencies = new[]
    {
        OrchardCore.OpenId.OpenIdConstants.Features.Server,
        OrchardCore.OpenId.OpenIdConstants.Features.Validation
    }
)]