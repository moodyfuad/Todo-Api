using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Responses
{
    public class PaginationHeader
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }

        public PaginationHeader() { }

        public PaginationHeader(PagedList<object> list) // not used directly; helper below will convert
        {
            CurrentPage = list.CurrentPage;
            PageSize = list.PageSize;
            TotalCount = list.TotalCount;
            TotalPages = list.TotalPages;
            HasPrevious = list.HasPrevious;
            HasNext = list.HasNext;
        }
    }
}
