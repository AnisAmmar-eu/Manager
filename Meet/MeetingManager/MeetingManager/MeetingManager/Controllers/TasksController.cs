using MeetingManager.Models;
using MeetingManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tasks task)
        {
            var result = await _service.CreateAsync(task);
            return Ok(result);
        }

        [HttpGet("by-meeting/{meetingId}")]
        public async Task<IActionResult> GetByMeeting(int meetingId)
        {
            var tasks = await _service.GetByMeetingIdAsync(meetingId);
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tasks task)
        {
            var updated = await _service.UpdateAsync(id, task);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}
