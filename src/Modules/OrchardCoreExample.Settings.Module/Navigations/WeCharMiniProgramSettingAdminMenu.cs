using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreExample.Settings.Module.Permissions;

namespace OrchardCoreExample.Settings.Module.Navigations
{
    public class WeCharMiniProgramSettingAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer T;

        public WeCharMiniProgramSettingAdminMenu(IStringLocalizer<WeCharMiniProgramSettingAdminMenu> localizer)
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
                .Add(T["微信小程序"], "10", weCharMiniConfiguration => weCharMiniConfiguration
                    .Add(T["参数设置"], "0", settings => settings
                        .Action("Index", "Admin",
                            new
                            {
                                area = "OrchardCore.Settings",
                                groupId = Features.OrchardCoreExampleSettingsModule
                            })
                        .Permission(WeCharMiniProgramSettingPermission.WeCharMiniProgramAccess)
                        .LocalNav()
                    )));

            return Task.CompletedTask;
        }
    }
}
