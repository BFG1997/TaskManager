using System.ComponentModel.DataAnnotations;
using TaskManager.Contracts.Enums;

namespace TaskManager.Contracts.DTO
{
    public class UpdateTaskStatusDto
    {
        [Required(ErrorMessage = "This field is required")]
        public Status Status { get; set; }
    }
}
