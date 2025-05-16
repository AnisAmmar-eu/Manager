using MeetingManager.Models;

namespace MeetingManager.Services
{
    public interface IMeetingService
    {
        Task<List<Meeting>> GetByProject(int projectId);
        Task<Meeting?> GetById(int id);
        Task<Meeting> Create(Meeting meeting);
        Task<Meeting> Update(int id, Meeting updated);
        Task Delete(int id);
        Task AssignMeetingToParticipant(int meetingId, int participantId);
    }
}
