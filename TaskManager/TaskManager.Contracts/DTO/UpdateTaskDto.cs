using TaskManager.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Contracts.DTO
{
    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Length must be between 3 and 80 characters")]
        public string Title { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Length must be between 0 and 500 characters")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public Priority Priority { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
