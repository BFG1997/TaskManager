using TaskManager.App.Models;
using TaskManager.Contracts.DTO;
using TaskManager.Contracts.Enums;

namespace TaskManager.App.Services.Interfaces
{
    public interface ITaskService
    {
        Task<PagedResult<TaskViewModel>> GetTasksAsync(
            int page = 1, 
            int pageSize = 10, 
            Status? status = null,
            Priority? priority = null,
            string? search = null,
            SortBy sortBy = SortBy.Title,
            SortDirection sortDir = SortDirection.Ascending
            );

        Task<TaskViewModel?> GetTaskById(int id);
        Task<HttpResponseMessage?> CreateTaskAsync(CreateTaskDto taskDto);
        Task<HttpResponseMessage?> UpdateTaskAsync(int id, UpdateTaskDto taskDto);
        Task<HttpResponseMessage?> UpdateTaskStatusAsync(int id, UpdateTaskStatusDto taskStatus);
        Task<HttpResponseMessage?> DeleteTaskAsync(int id);
    }
}
