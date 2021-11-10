using OrchardCore.Data.Migration;
using OrchardCoreExample.DataTable.Module.Indexes;
using YesSql.Sql;

namespace OrchardCoreExample.DataTable.Module.Migrations
{
    public class BookMigration : DataMigration
    {
        public int Create()
        {
            SchemaBuilder.CreateMapIndexTable<BookIndex>(table =>
            {
                table.Column<string>(nameof(BookIndex.Author));
                table.Column<string>(nameof(BookIndex.Title));
                table.Column<string>(nameof(BookIndex.CoverPhotoUrl));
                table.Column<string>(nameof(BookIndex.Description));
            });
            return 2;
        }

        public int UpdateFrom1()
        {
            // 在创建版本为1时执行,升级更新使用,上面返回的2,所以并不会执行
            SchemaBuilder.AlterTable(nameof(BookIndex), table =>
            {
                table.AddColumn<string>(nameof(BookIndex.CoverPhotoUrl));
                table.AddColumn<string>(nameof(BookIndex.Description));
            });
            return 2;
        }
    }
}
