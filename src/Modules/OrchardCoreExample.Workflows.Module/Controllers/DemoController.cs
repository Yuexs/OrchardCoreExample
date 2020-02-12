using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Workflows.Services;
using OrchardCoreExample.Workflows.Module.Workflows.Activities;

namespace OrchardCoreExample.Workflows.Module.Controllers
{
    [Route("api/demo2/[action]")]
    [Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
    [ApiController]
    public class DemoController: Controller
    {
        private readonly IWorkflowManager _workflowManager;

        public DemoController(IWorkflowManager workflowManager)
        {
            _workflowManager = workflowManager;
        }

        public IActionResult Test()
        {
            _workflowManager.TriggerEventAsync(nameof(TestEvent), new { json = "工作流测试" });
            return Json(new {result = "已经触发工作流事件"});
        }

    }
}
