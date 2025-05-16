using MeetingManager.Data;
using MeetingManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _db;

        public TaskService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Tasks> CreateAsync(Tasks task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return false;

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Tasks>> GetByMeetingIdAsync(int meetingId)
        {
            return await _db.Tasks
                .Where(t => t.MeetingId == meetingId)
                .ToListAsync();
        }

        public async Task<Tasks?> UpdateAsync(int id, Tasks task)
        {
            var existing = await _db.Tasks.FindAsync(id);
            if (existing == null) return null;

            existing.Description = task.Description;
            existing.Status = task.Status;
            existing.DueDate = task.DueDate;
            await _db.SaveChangesAsync();
            return existing;
        }
    }
}
