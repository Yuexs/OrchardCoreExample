using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;

namespace OrchardCoreExample.Workflows.Module.Workflows.Activities
{
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
}
