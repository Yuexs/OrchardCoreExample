using OrchardCore.DisplayManagement.Views;
using OrchardCore.Workflows.Display;
using OrchardCoreExample.Workflows.Module.Workflows.Activities;
using OrchardCoreExample.Workflows.Module.Workflows.ViewModels;

namespace OrchardCoreExample.Workflows.Module.Workflows.Drivers
{
    public class TestEventDisplay: ActivityDisplayDriver<TestEvent, TestEventViewModel>
    {
        protected override void EditActivity(TestEvent source, TestEventViewModel target)
        {
        }

        public override IDisplayResult Display(TestEvent activity)
        {
            return Combine(
                Shape("TestEvent_Fields_Thumbnail", new TestEventViewModel(activity)).Location("Thumbnail", "Content"),
                Factory("TestEvent_Fields_Design", ctx =>
                {
                    var shape = new TestEventViewModel();
                    shape.Activity = activity;

                    return shape;

                }).Location("Design", "Content")
            );
        }
    }
}
