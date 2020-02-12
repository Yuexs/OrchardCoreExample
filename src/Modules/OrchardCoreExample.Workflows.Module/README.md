# 工作流示例模块
> 模块定义了一个事件以及一个任务,
> 事件通过webapi触发

#### 1.创建`Manifest.cs`文件定义模块信息
> 工作流模块依赖`OrchardCore.Workflows`模块
```
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
```

#### 2.创建`TestEvent.cs`文件定义一个测试事件
> 工作流模块依赖`OrchardCore.Workflows`模块
```
public class TestEvent : Activity, IEvent
    {
        protected readonly IStringLocalizer S;

        public TestEvent(IStringLocalizer<TestEvent> localizer)
        {
            S = localizer;
        }

        public override string Name => nameof(TestEvent);
        public override LocalizedString DisplayText => S["测试事件"];
        public override LocalizedString Category => S["Demo"];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            return Outcomes(S["Done"]);
        }

        public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Halt();
        }

        public override ActivityExecutionResult Resume(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes("Done");
        }
    }
```
未完成