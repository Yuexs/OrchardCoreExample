using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCoreExample.DataTable.Module
{
    public class AdminMenu: INavigationProvider
    {
        private readonly IStringLocalizer T;
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder.Add(T["Book表测试"], "11", fxiaokeCrm =>
            {
                fxiaokeCrm.Add(T["Book表管理"], "0", codeSettings => codeSettings
                    .Action("Index", "Book", "OrchardCoreExample.DataTable.Module")
                    .LocalNav());
            });

            return Task.CompletedTask;
        }
    }
}
