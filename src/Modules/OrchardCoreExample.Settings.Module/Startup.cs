using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCoreExample.Settings.Module.Drivers;
using OrchardCoreExample.Settings.Module.Navigations;
using OrchardCoreExample.Settings.Module.Permissions;

namespace OrchardCoreExample.Settings.Module
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDisplayDriver<ISite>, WeCharMiniProgramSettingDisplayDriver>();
            services.AddScoped<IPermissionProvider, WeCharMiniProgramSettingPermission>();
            services.AddScoped<INavigationProvider, WeCharMiniProgramSettingAdminMenu>();
        }
    }
}
