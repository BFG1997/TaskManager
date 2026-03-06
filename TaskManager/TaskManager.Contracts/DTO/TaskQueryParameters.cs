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
        public bool? IsCompleted { get; set; }
        public Priority? Priority { get; set; }
        public string? Search { get; set; }
        public SortBy? SortBy { get; set; } = Enums.SortBy.CreatedAt;
        public SortDirection? SortDir { get; set; } = SortDirection.Ascending;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
