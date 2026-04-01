using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entities;
using TaskManager.Api.Repositories.Interfaces;
using TaskManager.Contracts.DTO;
using TaskManager.Contracts.Enums;

namespace TaskManager.Api.Repositories.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerDbContext _db;

        public TaskRepository(TaskManagerDbContext db)
        {
            _db = db;
        }

        public Task<TaskItem> CreateAsync(TaskItem item)
        {
            _db.Tasks.Add(item);
            return SaveAndReturn(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
                return false;

            _db.Tasks.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<TaskItem?> GetByIdAsync(int id)
        {
            return _db.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(TaskItem item)
        {
            var existing = await _db.Tasks.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (existing == null)
                return false;

            existing.Title = item.Title;
            existing.Description = item.Description;
            existing.Priority = item.Priority;
            existing.Status = item.Status;
            existing.DueDate = item.DueDate;
            existing.UpdatedAt = item.UpdatedAt;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(TaskItem item)
        {
            var existing = await _db.Tasks.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (existing == null)
                return false;

            existing.Status = item.Status;
            existing.UpdatedAt = item.UpdatedAt ?? DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<(IEnumerable<TaskItem>, int total)> GetAllAsync(TaskQueryParameters query)
        {
            var tasks = _db.Tasks.AsNoTracking().AsQueryable();

            if (query.Status.HasValue)
                tasks = tasks.Where(x => x.Status == query.Status.Value);

            if (query.Priority.HasValue)
                tasks = tasks.Where(x => x.Priority == query.Priority.Value);

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var term = query.Search.Trim();
                tasks = tasks.Where(x => EF.Functions.Like(x.Title, $"%{term}%"));
            }

            tasks = (query.SortBy, query.SortDir) switch
            {
                (SortBy.Title, SortDirection.Ascending) => tasks.OrderBy(x => x.Title),
                (SortBy.Title, SortDirection.Descending) => tasks.OrderByDescending(x => x.Title),
                (SortBy.DueDate, SortDirection.Ascending) => tasks.OrderBy(x => x.DueDate),
                (SortBy.DueDate, SortDirection.Descending) => tasks.OrderByDescending(x => x.DueDate),
                (SortBy.CreatedAt, SortDirection.Ascending) => tasks.OrderBy(x => x.CreatedAt),
                (SortBy.CreatedAt, SortDirection.Descending) => tasks.OrderByDescending(x => x.CreatedAt),
                (SortBy.UpdatedAt, SortDirection.Ascending) => tasks.OrderBy(x => x.UpdatedAt),
                (SortBy.UpdatedAt, SortDirection.Descending) => tasks.OrderByDescending(x => x.UpdatedAt),
                _ => tasks.OrderBy(x => x.CreatedAt)
            };

            var total = await tasks.CountAsync();

            var pageSize = Math.Clamp(query.PageSize, 1, 100);
            var page = query.Page < 1 ? 1 : query.Page;

            var items = await tasks
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        private async Task<TaskItem> SaveAndReturn(TaskItem item)
        {
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
