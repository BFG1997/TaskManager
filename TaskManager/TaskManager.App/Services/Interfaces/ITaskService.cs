using TaskManager.App.Models;
using TaskManager.Contracts.Common;
using TaskManager.Contracts.DTO;
using TaskManager.Contracts.Enums;

namespace TaskManager.App.Services.Interfaces
{
    public interface ITaskService
    {
        Task<PagedResult<TaskViewModel>> GetTasksAsync(
            int page = 1, 
            int pageSize = 10, 
            bool? isCompleted = null,
            Priority? priority = null,
            string? search = null,
            SortBy sortBy = SortBy.CreatedAt,
            SortDirection sortDir = SortDirection.Ascending
            );
    }
}
