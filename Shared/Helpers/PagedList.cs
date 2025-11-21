using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        private PagedList(IReadOnlyList<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        /// <summary>
        /// Create paged list from an IQueryable (executes the query).
        /// Note: pageNumber is 1-based.
        /// </summary>
        public static async Task<PagedList<T>> CreateAsync(
            IQueryable<T> source,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var count = await source.CountAsync(ct).ConfigureAwait(false);
            var skip = (pageNumber - 1) * pageSize;
            var items = await source.Skip(skip)
                .Take(pageSize)
                .ToListAsync(ct)
                .ConfigureAwait(false);

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
        public PagedList<TT> Map<TT>(Func<T, TT> expression) where TT : class
        {            

            return new PagedList<TT>([.. Items.Select(expression)], TotalCount, CurrentPage, PageSize);
        }
    }
}

