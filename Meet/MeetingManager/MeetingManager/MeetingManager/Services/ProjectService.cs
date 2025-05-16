using MeetingManager.Data;
using MeetingManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _db;

        public ProjectService(AppDbContext db) => _db = db;

        public async Task AssignProjectToParticipant(int projectId, int participantId)
        {
            // Find the participant
            var participant = await _db.Participants.FindAsync(participantId);
            if (participant == null)
            {
                throw new Exception($"Participant with ID {participantId} not found.");
            }

            // Verify the project exists
            var project = await _db.Projects.FindAsync(projectId);
            if (project == null)
            {
                throw new Exception($"Meeting with ID {projectId} not found.");
            }

            // Assign the project to the participant
            participant.ProjectId = projectId;
            _db.Participants.Update(participant);
            await _db.SaveChangesAsync();
        }

        public async Task<Project> CreateAsync(Project project)
        {
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();
            return project;
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return;
            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAll(int userId)
       => await _db.Projects.Where(p => p.OwnerId == userId).ToListAsync();


        public async Task<Project> GetById(int id)
        {
            return await _db.Projects.FindAsync(id);
        }

        public Task<Project> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> Update(int id, Project updated)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) throw new Exception("Not found");

            project.Title = updated.Title;
            project.Description = updated.Description;
            await _db.SaveChangesAsync();
            return project;
        }

 
    }

}
