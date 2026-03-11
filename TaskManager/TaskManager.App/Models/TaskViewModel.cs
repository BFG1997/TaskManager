using TaskManager.Contracts.Enums;

namespace TaskManager.App.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public string StatusLabel => IsCompleted ? "Completed" : "Not completed";
        public Priority Priority { get; set; } = Priority.Low;

        public string PriorityLabel => Priority switch
        {
            Priority.High => "Высокий",
            Priority.Medium => "Средний",
            Priority.Low => "Низкий",
            _ => ""
        };

        public string PriorityCssClass => Priority switch
        {
            Priority.High => "text-danger",
            Priority.Medium => "text-warning",
            Priority.Low => "text-success",
            _ => ""
        };

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DueDate { get; set; }

        public string CreatedAtFormatted => CreatedAt.ToString("dd.MM.yyyy HH:mm");
        public string? UpdatedAtFormatted => UpdatedAt?.ToString("dd.MM.yyyy HH:mm");
        public string? DueDateFormatted => DueDate?.ToString("dd.MM.yyyy");


    }
}
