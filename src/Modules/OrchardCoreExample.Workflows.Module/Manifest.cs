using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Workflows.Module",
    Author = "Yuex.S",
    Website = "https://gitee.com/YuexS/OrchardCoreExample",
    Version = "1.0.0",
    Description = "工作流模块",
    Category = "Example",
    Dependencies = new[]
    {
        "OrchardCore.Workflows"
    }
)]