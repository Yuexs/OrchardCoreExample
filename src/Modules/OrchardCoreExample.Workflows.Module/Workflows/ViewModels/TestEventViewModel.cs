using OrchardCore.Workflows.ViewModels;
using OrchardCoreExample.Workflows.Module.Workflows.Activities;

namespace OrchardCoreExample.Workflows.Module.Workflows.ViewModels
{
    public class TestEventViewModel: ActivityViewModel<TestEvent>
    {
        public TestEventViewModel()
        {
        }

        public TestEventViewModel(TestEvent activity)
        {
            Activity = activity;
        }
    }
}
