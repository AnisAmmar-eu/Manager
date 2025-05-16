using MeetingManager.Models;

namespace MeetingManager.Services
{
    public interface ITaskService
    {
        Task<Tasks> CreateAsync(Tasks task);
        Task<List<Tasks>> GetByMeetingIdAsync(int meetingId);
        Task<Tasks?> UpdateAsync(int id, Tasks task);
        Task<bool> DeleteAsync(int id);

    }
}
