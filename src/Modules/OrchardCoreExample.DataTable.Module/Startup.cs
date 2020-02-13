using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCoreExample.DataTable.Module.Migrations;

namespace OrchardCoreExample.DataTable.Module
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, BookMigration>();
        }
    }
}
