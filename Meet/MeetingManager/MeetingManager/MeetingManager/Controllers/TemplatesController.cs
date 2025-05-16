using MeetingManager.Data;
using MeetingManager.Models;
using MeetingManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplatesController : Controller
    {
        private readonly ITemplateService _service;
        private readonly AppDbContext _context;

        public TemplatesController(ITemplateService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var template = await _service.GetById(id);
            if (template == null) return NotFound();
            return Ok(template);
        }
        [HttpGet("templates")]
        public IActionResult GetTemplates()
        {
            return Ok(_context.Templates.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Templates template)
        {
            var created = await _service.Create(template);
            return Ok(created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Templates updated)
        {
            var result = await _service.Update(id, updated);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }

    }
}
