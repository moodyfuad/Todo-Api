using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.Pagination
{
    public class PersonSearchParameters : PaginationParameters
    {
        public string? Username { get; set; }
        //public string? FullName { get; set; }
    }
}
