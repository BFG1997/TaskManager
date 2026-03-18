using TaskManager.Api.Entities;
using TaskManager.Contracts.DTO;

namespace TaskManager.Api.Mappers
{
    public static class TaskMapper
    {
        public static TaskDto Map(TaskItem taskItem)
        {
            return new TaskDto()
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                Status = taskItem.Status,
                Priority = taskItem.Priority,
                DueDate = taskItem.DueDate,
                CreatedAt = taskItem.CreatedAt,
                UpdatedAt = taskItem.UpdatedAt,
            };
        }

        public static TaskItem Map(TaskDto taskDto)
        {
            return new TaskItem()
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
                DueDate = taskDto.DueDate,
                CreatedAt = taskDto.CreatedAt,
                UpdatedAt = taskDto.UpdatedAt,
            };
        }

        public static TaskItem Map(CreateTaskDto createTaskDto)
        {
            return new TaskItem()
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Priority = createTaskDto.Priority,
                DueDate = createTaskDto.DueDate,
                CreatedAt = DateTime.UtcNow,
                Status = Contracts.Enums.Status.Created
            };
        }
    }
}
