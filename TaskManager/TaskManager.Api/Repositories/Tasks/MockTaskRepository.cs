using TaskManager.Api.Entities;
using TaskManager.Api.Repositories.Interfaces;
using TaskManager.Contracts.DTO;
using TaskManager.Contracts.Enums;

namespace TaskManager.Api.Repositories.Tasks
{
    public class MockTaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new();
        private int _currentId = 1;

        public Task<TaskItem> CreateAsync(TaskItem item)
        {
            item.Id = _currentId++;
            _tasks.Add(item);

            return Task.FromResult(item);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);  
            if (task == null)
                return Task.FromResult(false);

            _tasks.Remove(task);
            return Task.FromResult(true);
        }

        public Task<(IEnumerable<TaskItem>, int total)> GetAllAsync(TaskQueryParameters query)
        {
            var tasks = _tasks.AsQueryable();

            if (query.IsCompleted.HasValue)
                tasks = tasks.Where(x => x.IsCompleted == query.IsCompleted.Value);

            if (query.Priority.HasValue)
                tasks = tasks.Where(x => x.Priority == query.Priority.Value);   

            if (!string.IsNullOrEmpty(query.Search))
                tasks = tasks.Where(x => x.Title.Contains(query.Search, StringComparison.OrdinalIgnoreCase));

            tasks = (query.SortBy, query.SortDir) switch
            {
                (SortBy.DueDate, SortDirection.Ascending) => tasks.OrderBy(x => x.DueDate),
                (SortBy.DueDate, SortDirection.Descending) => tasks.OrderByDescending(x => x.DueDate),
                (SortBy.CreatedAt, SortDirection.Ascending) => tasks.OrderBy(x => x.CreatedAt),
                (SortBy.CreatedAt, SortDirection.Descending) => tasks.OrderByDescending(x => x.CreatedAt),
                (SortBy.UpdatedAt, SortDirection.Ascending) => tasks.OrderBy(x => x.UpdatedAt),
                (SortBy.UpdatedAt, SortDirection.Descending) => tasks.OrderByDescending(x => x.UpdatedAt),
                _ => tasks.OrderBy(x => x.CreatedAt)
            };

            var total = tasks.Count();

            var pageSize = Math.Clamp(query.PageSize, 1, 100);
            var page = query.Page < 1 ? 1 : query.Page;

            var items = tasks
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();  

            return Task.FromResult((items.AsEnumerable(), total));
        }

        public Task<TaskItem?> GetByIdAsync(int id)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == id);

            return Task.FromResult(task);
        }

        public Task<bool> UpdateAsync(TaskItem item)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == item.Id);

            if (task == null)
                return Task.FromResult(false);

            task.Title = item.Title;
            task.Description = item.Description;
            task.Priority = item.Priority;
            task.IsCompleted = item.IsCompleted;
            task.DueDate = item.DueDate;
            task.UpdatedAt = item.UpdatedAt;

            return Task.FromResult(true);
        }

        public Task<bool> UpdateStatusAsync(TaskItem item)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == item.Id);

            if (task == null)
                return Task.FromResult(false);

            task.IsCompleted = item.IsCompleted;
            task.UpdatedAt = DateTime.UtcNow;

            return Task.FromResult(true);
        }
    }
}
