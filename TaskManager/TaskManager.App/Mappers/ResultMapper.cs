using TaskManager.App.Models;
using TaskManager.Contracts.DTO;

namespace TaskManager.App.Mappers
{
    public static class ResultMapper
    {
        public static TaskViewModel Map(TaskDto taskDto)
        {
            return new TaskViewModel
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
                CreatedAt = taskDto.CreatedAt,
                UpdatedAt = taskDto.UpdatedAt,
                DueDate = taskDto.DueDate
            };
        }
    }
}
