using TaskManager.Api.Mappers;
using TaskManager.Api.Repositories.Interfaces;
using TaskManager.Contracts.Common;
using TaskManager.Contracts.DTO;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll([FromQuery] TaskQueryParameters query)
        {
            var (tasks, total) = await _taskRepository.GetAllAsync(query);
            var response = new PagedResponse<TaskDto>
            {
                Items = tasks.Select(TaskMapper.Map),
                Page = query.Page,
                PageSize = Math.Clamp(query.PageSize, 0, 100),
                Total = total
            };

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskDto>> GetById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null) 
                return NotFound();

            return Ok(TaskMapper.Map(task));
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> Create(CreateTaskDto createTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = TaskMapper.Map(createTaskDto);
            var createdTask = await _taskRepository.CreateAsync(task);

            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, TaskMapper.Map(createdTask));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateTaskDto updateTaskDto)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            task.Title = updateTaskDto.Title;
            task.Description = updateTaskDto.Description;
            task.Priority = updateTaskDto.Priority; 
            task.Status = updateTaskDto.Status;
            task.DueDate = updateTaskDto.DueDate;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(task);    

            return NoContent();
        }

        [HttpPatch("{id:int}/complete")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateTaskStatusDto updateTaskStatusDto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            task.Status = updateTaskStatusDto.Status;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateStatusAsync(task);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            await _taskRepository.DeleteAsync(id);

            return NoContent();
        }

    }
}
