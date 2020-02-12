using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Workflows.Helpers;
using OrchardCoreExample.Workflows.Module.Workflows.Activities;
using OrchardCoreExample.Workflows.Module.Workflows.Drivers;

namespace OrchardCoreExample.Workflows.Module.Workflows
{
    public class Startup: StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<TestTask, TestTaskDisplay>();
            services.AddActivity<TestEvent, TestEventDisplay>();
        }
    }
}
