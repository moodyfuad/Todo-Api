using Shared.Helpers;
using System.Text.Json;

namespace API.Extensions
{
    public static class ResponseExtensions
    {
        private const string PaginationHeaderName = "X-Pagination";
        private const string ExposeHeadersHeaderName = "Access-Control-Expose-Headers";

        public static void AddPaginationHeader<T>(this HttpResponse response, PagedList<T> pagedList)
        {
            var header = new
            {
                pagedList.CurrentPage,
                pagedList.PageSize,
                pagedList.TotalCount,
                pagedList.TotalPages,
                pagedList.HasPrevious,
                pagedList.HasNext
            };

            var json = JsonSerializer.Serialize(header);
            if (!response.Headers.ContainsKey(PaginationHeaderName))
                response.Headers.Append(PaginationHeaderName, json);

            // for CORS: expose header to browser
            if (response.Headers.ContainsKey(ExposeHeadersHeaderName))
            {
                var existing = response.Headers[ExposeHeadersHeaderName].ToString();
                if (!existing.Contains(PaginationHeaderName))
                    response.Headers[ExposeHeadersHeaderName] = $"{existing}, {PaginationHeaderName}";
            }
            else
            {
                response.Headers.Append(ExposeHeadersHeaderName, PaginationHeaderName);
            }
        }
    }
}
