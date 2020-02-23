using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Permissions;

namespace OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Navigations
{
    public class MiniProgramAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer T;

        public MiniProgramAdminMenu(IStringLocalizer<MiniProgramAdminMenu> localizer)
        {
            T = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase)) return Task.CompletedTask;

            builder.Add(T["Configuration"], configuration => configuration
                .Add(T["微信小程序设置"], "20", weCharMiniConfiguration => weCharMiniConfiguration
                    .Add(T["参数设置"], "0", settings => settings
                        .Action("Index", "Admin",
                            new
                            {
                                area = "OrchardCore.Settings",
                                groupId = Features.WeCharMiniProgram
                            })
                        .Permission(MiniProgramPermission.WeCharMiniProgramAccess)
                        .LocalNav()
                    )));

            return Task.CompletedTask;
        }
    }
}