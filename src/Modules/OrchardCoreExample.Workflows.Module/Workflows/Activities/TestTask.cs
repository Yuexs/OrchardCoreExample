using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;

namespace OrchardCoreExample.Workflows.Module.Workflows.Activities
{
    public class TestTask: TaskActivity
    {
        private readonly IStringLocalizer<TestTask> S;

        public TestTask(IStringLocalizer<TestTask> localizer)
        {
            S = localizer;
        }

        public override string Name => nameof(TestTask);
        public override LocalizedString DisplayText => S["测试任务"];
        public override LocalizedString Category => S["Demo"];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"]);
        }

        public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            // 执行任务
            // workflowContext.Input 获取事件参数
            return Outcomes("Done");
        }
    }
}
