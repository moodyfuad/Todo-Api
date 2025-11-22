using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.Pagination
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get;
            set;
        }
    }
}
