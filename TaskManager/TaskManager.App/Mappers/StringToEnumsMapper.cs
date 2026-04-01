using TaskManager.Contracts.Enums;

namespace TaskManager.App.Mappers
{
    public static class StringToEnumsMapper
    {
        public static SortBy MapToSortBy(string? sortBy)
        {
            switch (sortBy)
            {
                case "CreatedAt":
                    return SortBy.CreatedAt;

                case "UpdatedAt":
                    return SortBy.UpdatedAt;

                case "DueDate":
                    return SortBy.DueDate;

                case "Title":
                    return SortBy.Title;

                default:
                    return SortBy.Title;
            }
        }

        public static SortDirection MapToSortDirection(string? sortDirection)
        {
            switch (sortDirection)
            {
                case "Ascending":
                    return SortDirection.Ascending;

                case "Descending":
                    return SortDirection.Descending;

                default:
                    return SortDirection.Ascending;
            }
        }

        public static Priority? MapToPriority(string? priority)
        {
            switch (priority)
            {
                case "Low":
                    return Priority.Low;
                case "Medium":
                    return Priority.Medium;
                case "High":
                    return Priority.High;
                default:
                    return null;
            }
        }

        public static Status? MapToStatus(string? status)
        {
            switch (status)
            {
                case "Created":
                    return Status.Created;
                case "InProgress":
                    return Status.InProgress;
                case "Done":
                    return Status.Done;
                default:
                    return null;
            }
        }
    }
}
