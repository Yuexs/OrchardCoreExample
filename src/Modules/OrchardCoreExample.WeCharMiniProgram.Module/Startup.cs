using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Entities;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Drivers;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Models;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Navigations;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Permissions;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.MP;
using Senparc.Weixin.RegisterServices;

namespace OrchardCoreExample.WeCharMiniProgram.Module
{
    public class Startup : StartupBase
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override async void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes,
            IServiceProvider serviceProvider)
        {
            var settings = (await serviceProvider.GetService<ISiteService>().GetSiteSettingsAsync())
                .As<MiniProgramSettingModel>();

            // 模块第一次加载时没有参数设置,这里不加载,设置参数后加载
            if (settings.WxOpenAppId == null && settings.WxOpenAppSecret == null) return;
            var register = RegisterService.Start(new SenparcSetting
            {
                IsDebug = true,
                DefaultCacheNamespace = "DefaultCache"
            });
            register.UseSenparcGlobal(true);
            register.RegisterMpAccount(settings.WxOpenAppId, settings.WxOpenAppSecret, null);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSenparcGlobalServices(_configuration);
            services.AddSenparcWeixinServices(_configuration);
            services.AddScoped<IDisplayDriver<ISite>, MiniProgramSettingDisplayDriver>();
            services.AddScoped<IPermissionProvider, MiniProgramPermission>();
            services.AddScoped<INavigationProvider, MiniProgramAdminMenu>();
        }
    }
}