using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskmanagementapp.Exceptions;
using taskmanagementapp.Models;
using taskmanagementapp.Services;

namespace taskmanagementapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly IColumnService _columnService;

        public ColumnController(IColumnService columnService)
        {
            _columnService = columnService;
        }

        [HttpPost]
        public async Task<IActionResult> AddColumn(ColumnDto columnDto)
        {
            try
            {
                await _columnService.AddColumnAsync(columnDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the column.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateColumn(int id, ColumnDto columnDto)
        {
            try
            {
                await _columnService.UpdateColumnAsync(id, columnDto);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the column.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumn(int id)
        {
            try
            {
                await _columnService.DeleteColumnAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the column.");
            }
        }
    }
}
