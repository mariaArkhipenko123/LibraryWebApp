using System.Collections.Generic;

namespace Library.Application.Library.Queries.GetLibraryList
{
    public class LibraryListVm
    {
        public IList<LibraryLookupDto> Books { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
    }
}
