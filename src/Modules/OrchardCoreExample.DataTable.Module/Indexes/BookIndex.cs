using YesSql.Indexes;

namespace OrchardCoreExample.DataTable.Module.Indexes
{
    public class BookIndex : MapIndex
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string Description { get; set; }
    }
}
