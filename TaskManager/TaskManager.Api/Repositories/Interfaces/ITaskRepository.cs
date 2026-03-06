using TaskManager.Api.Entities;
using TaskManager.Api.Mappers;
using TaskManager.Contracts.DTO;

namespace TaskManager.Api.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateAsync(TaskItem item);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<(IEnumerable<TaskItem>, int total)> GetAllAsync(TaskQueryParameters query);
        Task<bool> UpdateAsync(TaskItem item);  
        Task<bool> UpdateStatusAsync(TaskItem item);
        Task<bool> DeleteAsync(int id);
    }
}
