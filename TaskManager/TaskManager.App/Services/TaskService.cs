using TaskManager.App.Mappers;
using TaskManager.App.Models;
using TaskManager.App.Services.Interfaces;
using TaskManager.Contracts.Common;
using TaskManager.Contracts.DTO;
using TaskManager.Contracts.Enums;
using System.Net;

namespace TaskManager.App.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage?> CreateTaskAsync(CreateTaskDto taskDto)
        {
            return await _httpClient.PostAsJsonAsync("api/tasks", taskDto);
        }

        public async Task<HttpResponseMessage?> DeleteTaskAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/tasks/{id}");
        }

        public async Task<TaskViewModel?> GetTaskById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskDto>($"api/tasks/{id}");

            if (result == null)
                return new TaskViewModel();

            return ResultMapper.Map(result);
        }

        public async Task<PagedResult<TaskViewModel>> GetTasksAsync(
            int page = 1, 
            int pageSize = 10, 
            Status? status = null, 
            Priority? priority = null,
            string? search = null,
            SortBy sortBy = SortBy.Title,
            SortDirection sortDir = SortDirection.Ascending)
        {
            var uri = $"api/tasks?page={page}&pageSize={pageSize}&sortBy={sortBy}&sortDir={sortDir}";
            uri = AddOptionalQuery(uri, "status", status?.ToString());
            uri = AddOptionalQuery(uri, "priority", priority?.ToString());
            uri = AddOptionalQuery(uri, "search", search);

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

        public async Task<HttpResponseMessage?> UpdateTaskAsync(int id, UpdateTaskDto taskDto)
        {
            return await _httpClient.PutAsJsonAsync($"api/tasks/{id}", taskDto);
        }

        public async Task<HttpResponseMessage?> UpdateTaskStatusAsync(int id, UpdateTaskStatusDto taskStatus)
        {
            return await _httpClient.PatchAsJsonAsync($"api/tasks/{id}/complete", taskStatus);
        }

        private static string AddOptionalQuery(string uri, string key, string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return uri;

            return $"{uri}&{key}={WebUtility.UrlEncode(value)}";
        }
    }
}
