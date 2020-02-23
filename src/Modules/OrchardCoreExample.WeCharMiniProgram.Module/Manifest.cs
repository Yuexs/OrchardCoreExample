using OrchardCore.Modules.Manifest;
using OrchardCoreExample.WeCharMiniProgram.Module;

[assembly: Module(
    Name = Features.WeCharMiniProgram,
    Author = "Yuex.S",
    Website = "https://gitee.com/YuexS/OrchardCoreExample",
    Version = "1.0.0",
    Description = "微信小程序插件",
    Category = "Example",
    Dependencies = new[]
    {
        "OrchardCore.OpenId.Server",
        "OrchardCore.OpenId.Validation"
    }
)]