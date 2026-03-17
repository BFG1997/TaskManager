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
            SortBy sortBy = SortBy.Title,
            SortDirection sortDir = SortDirection.Ascending
            );

        Task<HttpResponseMessage?> CreateTaskAsync(CreateTaskDto taskDto);
        Task<HttpResponseMessage?> UpdateTaskAsync(int id, UpdateTaskDto taskDto);
        Task<HttpResponseMessage?> UpdateTaskStatusAsync (int id, UpdateTaskStatusDto taskStatus);
        Task<HttpResponseMessage?> DeleteTaskAsync(int id);
    }
}
