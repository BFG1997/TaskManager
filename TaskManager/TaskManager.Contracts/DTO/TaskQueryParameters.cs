using TaskManager.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Contracts.DTO
{
    public class TaskQueryParameters
    {
        public Status? Status { get; set; } 
        public Priority? Priority { get; set; }
        public string? Search { get; set; }
        public SortBy? SortBy { get; set; } = Enums.SortBy.Title;
        public SortDirection? SortDir { get; set; } = SortDirection.Ascending;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
