using System.Collections.Generic;

namespace OrchardCoreExample.DataTable.Module.ViewModels
{
    public class BookIndexViewModel
    {
        public IList<BookModelEntry> Books { get; } = new List<BookModelEntry>();
        public dynamic Pager { get; set; }
    }

    public class BookModelEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string Description { get; set; }
    }
}
