namespace TaskManager.App.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages
        {
            get
            {
                var total = (int)Math.Ceiling((double)TotalCount / PageSize);
                if (total == 0)
                    return 1;
                return total;
            }
        }

    }
}
