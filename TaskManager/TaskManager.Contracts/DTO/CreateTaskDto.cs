using TaskManager.Contracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Contracts.DTO
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Length must be between 3 and 80 characters")]
        public string Title { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Length must be between 0 and 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public Priority Priority { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
