using TaskManager.Contracts.Enums;

namespace TaskManager.App.Mappers
{
    public static class EnumsToStringMapper
    {
        public static string Map(SortBy sortBy)
        {
            switch (sortBy)
            {
                case SortBy.CreatedAt:
                    return "CreatedAt";

                case SortBy.UpdatedAt:
                    return "UpdatedAt";

                case SortBy.DueDate:
                    return "DueDate";

                case SortBy.Title:
                    return "Title";

                default:
                    return string.Empty;
            }
        }

        public static string Map(SortDirection sortDirection)
        {
            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    return "asc";

                case SortDirection.Descending:
                    return "desc";

                default:
                    return string.Empty;
            }
        }

        public static string Map(Priority priority)
        {
            switch (priority)
            {
                case Priority.Low:
                    return "Low";
                case Priority.Medium:
                    return "Medium";
                case Priority.High:
                    return "High";
                default:
                    return string.Empty;
            }
        }
    }
}
