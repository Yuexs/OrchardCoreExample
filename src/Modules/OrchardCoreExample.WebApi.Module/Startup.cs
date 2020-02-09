using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Security.Permissions;
using OrchardCoreExample.WebApi.Module.Permissions;

namespace OrchardCoreExample.WebApi.Module
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionProvider, WebApiPermission>();
        }
    }
}
