using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCoreExample.DataTable.Module.Migrations;
using OrchardCoreExample.DataTable.Module.Provider;
using YesSql.Indexes;

namespace OrchardCoreExample.DataTable.Module
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddSingleton<IIndexProvider, BookIndexProvider>();
            services.AddScoped<IDataMigration, BookMigration>();
        }
    }
}
