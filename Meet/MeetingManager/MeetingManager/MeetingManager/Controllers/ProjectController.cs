using MeetingManager.Data;
using MeetingManager.Models;
using MeetingManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;
    private readonly AppDbContext _context;

    public ProjectsController(IProjectService service, AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpPost("{projectId}/assign-participant/{participantId}")]
    public async Task<IActionResult> AssignParticipant(int projectId, int participantId)
    {
        // Assigner le participant à la réunion
        await _service.AssignProjectToParticipant(projectId, participantId);


        var project = await _context.Projects.FindAsync(projectId);
        var participant = await _context.Participants.FindAsync(participantId);

        if (project == null || participant == null)
        {
            return NotFound("Project or Participant not found.");
        }


        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await _service.GetByIdAsync(id);
        if (project == null) return NotFound();
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync(); // ID généré

        if (project.Participants != null && project.Participants.Any())
        {
            foreach (var participant in project.Participants)
            {
                participant.ProjectId = project.Id;
                _context.Participants.Add(participant);
            }
            await _context.SaveChangesAsync();
        }

        return Ok(project);
    }



    [HttpGet("projs")]
    public IActionResult GetProjects()
    {
        return Ok(_context.Projects.ToList());
    }
    [HttpPost("{projectId}/assign-participant")]
    public async Task<IActionResult> AssignParticipant(int projectId, [FromBody] Participant participant)
    {
        participant.ProjectId = projectId;
        _context.Participants.Add(participant);
        await _context.SaveChangesAsync();
        return Ok(participant);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Project updated)
    {
        var result = await _service.Update(id, updated);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}