using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskmanagementapp.Exceptions;
using taskmanagementapp.Models;
using taskmanagementapp.Services;

namespace taskmanagementapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskDto taskDto)
        {
            await _taskService.AddTaskAsync(taskDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskDto taskDto)
        {
            try
            {
                await _taskService.UpdateTaskAsync(id, taskDto);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the task.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the task.");
            }
        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> AddImageToTask(int id, [FromBody] string imageUrl)
        {
            try
            {
                await _taskService.AddImageToTaskAsync(id, imageUrl);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while attaching the image to the task.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task == null)
                    return NotFound();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the task details.");
            }
        }

        [HttpPost("{taskId}/move/{columnId}")]
        public async Task<IActionResult> MoveTaskToColumn(int taskId, int columnId)
        {
            try
            {
                await _taskService.MoveTaskToColumnAsync(taskId, columnId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while moving the task to the column.");
            }
        }
    }
}
