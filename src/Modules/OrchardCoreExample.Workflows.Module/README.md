# 工作流示例模块
> 模块定义了一个事件以及一个任务,
> 事件通过webapi触发

#### 1.事件触发
> 事件触发使用webapi\
> 开发者可以参考更换为其他方式,这里仅供测试\
> 地址http://127.0.0.1:5000/api/demo2/test

DemoController.cs文件内容
```
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

```

#### 2.任务执行
> TestTask.cs文件内根据触发进行操作
```
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
```
#### 3.安装模块并设置流程
> 添加一个事件(测试事件)和任务(测试任务)\
> 启用测试事件,并且添加流程线到测试任务

![image](https://gitee.com/YuexS/OrchardCoreExample/raw/master/imgs/WX20200213-035846@2x.png)

#### 4.访问api,将会触发当前流程