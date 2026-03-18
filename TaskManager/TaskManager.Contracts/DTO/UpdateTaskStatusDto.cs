using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Contracts.Enums;

namespace TaskManager.Contracts.DTO
{
    public class UpdateTaskStatusDto
    {
        [Required(ErrorMessage = "This field is required")]
        public Status Status { get; set; }
    }
}
