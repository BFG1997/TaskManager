using TaskManager.App.Mappers;
using TaskManager.App.Models;
using TaskManager.App.Services.Interfaces;
using TaskManager.Contracts.Common;
using TaskManager.Contracts.DTO;
using TaskManager.Contracts.Enums;

namespace TaskManager.App.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedResult<TaskViewModel>> GetTasksAsync(
            int page = 1, 
            int pageSize = 10, 
            bool? isCompleted = null, 
            Priority? priority = null,
            string? search = null,
            SortBy sortBy = SortBy.CreatedAt,
            SortDirection sortDir = SortDirection.Ascending)
        {
            var uri = $"api/tasks?page={page}&pageSize={pageSize}&sortBy={sortBy}&sortDir={sortDir}";

            if (isCompleted.HasValue)
                uri += $"&isCompleted={isCompleted.Value}";

            if (priority.HasValue)
                uri += $"&priority={priority.Value}";

            if (!string.IsNullOrWhiteSpace(search))
                uri += $"&priority={search}";

            var result = await _httpClient.GetFromJsonAsync<PagedResponse<TaskDto>>(uri);

            if (result == null)
                return new PagedResult<TaskViewModel>();

            return new PagedResult<TaskViewModel>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.Total,
                Items = result.Items.Select(ResultMapper.Map).ToList()
            };
        }
    }
}
