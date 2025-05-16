using MeetingManager.Models;

namespace MeetingManager.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAll(int userId);
        Task<Project> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project project);
        Task<Project> Update(int id, Project updated);
        Task DeleteAsync(int id);
        Task AssignProjectToParticipant(int projectId, int participantId);

    }

}
