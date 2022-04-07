using OrchardCoreExample.AliYunSecurityTokenService.Module.Drivers;
using OrchardCoreExample.AliYunSecurityTokenService.Module.Navigations;
using OrchardCoreExample.AliYunSecurityTokenService.Module.Permissions;
using OrchardCoreExample.AliYunSecurityTokenService.Module.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCore.Swagger.Abstractions;

namespace OrchardCoreExample.AliYunSecurityTokenService.Module
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IDisplayDriver<ISite>, AliYunSecurityTokenServiceSettingDisplayDriver>();
            services.AddScoped<IPermissionProvider, AliYunSecurityTokenServicePermission>();
            services.AddScoped<INavigationProvider, AliYunSecurityTokenServiceSettingAdminMenu>();
            services.AddScoped<IAliYunSecurityTokenService, Services.AliYunSecurityTokenService>();
            services.AddTransient<ISwaggerApiDefinition, ApiDefinition>();
        }
    }
}
