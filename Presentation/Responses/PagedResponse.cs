using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Responses
{
    /// <summary>
    /// Paged response wrapper that exposes pagination metadata in the response body.
    /// </summary>
    public class PagedResponse<T> : ApiResponse<IEnumerable<T>>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }

        public PagedResponse(PagedList<T> pagedList, string? message = null)
            : base(pagedList.Items, success: true, message: message)
        {
            CurrentPage = pagedList.CurrentPage;
            PageSize = pagedList.PageSize;
            TotalCount = pagedList.TotalCount;
            TotalPages = pagedList.TotalPages;
            HasPrevious = pagedList.HasPrevious;
            HasNext = pagedList.HasNext;
        }
    }
}
