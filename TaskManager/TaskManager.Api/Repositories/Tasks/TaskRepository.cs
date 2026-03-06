using TaskManager.Api.Entities;
using TaskManager.Api.Repositories.Interfaces;
using TaskManager.Contracts.DTO;

namespace TaskManager.Api.Repositories.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        public Task<TaskItem> CreateAsync(TaskItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskItem?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TaskItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStatusAsync(TaskItem item)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<TaskItem>, int total)> GetAllAsync(TaskQueryParameters query)
        {
            throw new NotImplementedException();
        }
    }
}
