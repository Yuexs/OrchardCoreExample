using System;
using System.Threading.Tasks;
using OrchardCoreExample.AliYunSecurityTokenService.Module.Permissions;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCoreExample.AliYunSecurityTokenService.Module.Navigations
{
    public class AliYunSecurityTokenServiceSettingAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer T;

        public AliYunSecurityTokenServiceSettingAdminMenu(IStringLocalizer<AliYunSecurityTokenServiceSettingAdminMenu> localizer)
        {
            T = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder.Add(T["Configuration"], configuration => configuration
                .Add(T["阿里云STS授权接口"], "20", aliYunSecurityTokenService => aliYunSecurityTokenService
                    .Add(T["参数设置"], "0", settings => settings
                        .Action("Index", "Admin",
                            new
                            {
                                area = "OrchardCore.Settings",
                                groupId = Features.AliYunSecurityTokenServiceAuthentication
                            })
                        .Permission(AliYunSecurityTokenServicePermission.AliYunSecurityTokenServiceAccess)
                        .LocalNav()
                    )));
            return Task.CompletedTask;
        }
    }
}
