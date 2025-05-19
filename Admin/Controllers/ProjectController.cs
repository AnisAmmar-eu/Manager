﻿using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    using Admin.Data;
    using Admin.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;


    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProjectController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var userId = Guid.Parse(User.Identity!.Name!);
            var projects = await _db.ProjectUsers
                .Where(pu => pu.UserId == userId)
                .Select(pu => pu.Project)
                .ToListAsync();
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            project.Id = Guid.NewGuid();

            _db.Projects.Add(project);

            // Add current user as participant by default
            var userId = Guid.Parse(User.Identity!.Name!);
            _db.ProjectUsers.Add(new ProjectUser { ProjectId = project.Id, UserId = userId });

            await _db.SaveChangesAsync();

            return Ok(project);
        }

        [HttpPost("{projectId}/addParticipant/{userId}")]
        public async Task<IActionResult> AddParticipant(Guid projectId, Guid userId)
        {
            var exists = await _db.ProjectUsers.AnyAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);
            if (exists) return BadRequest("Participant already added.");

            _db.ProjectUsers.Add(new ProjectUser { ProjectId = projectId, UserId = userId });
            await _db.SaveChangesAsync();

            return Ok();
        }
    }

}
