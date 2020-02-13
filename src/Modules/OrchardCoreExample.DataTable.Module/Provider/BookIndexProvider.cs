using OrchardCoreExample.DataTable.Module.Indexes;
using OrchardCoreExample.DataTable.Module.Models;
using YesSql.Indexes;

namespace OrchardCoreExample.DataTable.Module.Provider
{
    public class BookIndexProvider : IndexProvider<BookModel>
    {
        public override void Describe(DescribeContext<BookModel> context)
        {
            context.For<BookIndex>()
                .Map(book =>
                    new BookIndex
                    {
                        Author = book.Author,
                        Title = book.Title,
                        CoverPhotoUrl = book.CoverPhotoUrl,
                        Description = book.Description
                    });
        }
    }
}
