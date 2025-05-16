using MeetingManager.Data;
using MeetingManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingManager.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly AppDbContext _db;

        public MeetingService(AppDbContext db) => _db = db;

        public async Task AssignMeetingToParticipant(int meetingId, int participantId)
        {
            // Find the participant
            var participant = await _db.Participants.FindAsync(participantId);
            if (participant == null)
            {
                throw new Exception($"Participant with ID {participantId} not found.");
            }

            // Verify the meeting exists
            var meeting = await _db.Meetings.FindAsync(meetingId);
            if (meeting == null)
            {
                throw new Exception($"Meeting with ID {meetingId} not found.");
            }

            // Assign the meeting to the participant
            participant.MeetingId = meetingId;
            _db.Participants.Update(participant);
            await _db.SaveChangesAsync();
        
        }

        public async Task<Meeting> Create(Meeting meeting)
        {
            string roomName = "meeting-" + Guid.NewGuid().ToString("N").Substring(0, 8);

            // Génère le lien Jitsi
            meeting.Link = $"https://meet.jit.si/{roomName}";

            _db.Meetings.Add(meeting);
            await _db.SaveChangesAsync();
            return meeting;
        }




        public async Task Delete(int id)
        {
            var meeting = await _db.Meetings.FindAsync(id);
            if (meeting == null) return;
            _db.Meetings.Remove(meeting);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var meeting = await _db.Meetings.FindAsync(id);
            if (meeting == null) return;
            _db.Meetings.Remove(meeting);
            await _db.SaveChangesAsync();
        }

        public async Task<Meeting?> GetById(int id)
       
             => await _db.Meetings
            .Include(m => m.Participants)
            .Include(m => m.AgendaItems)
            .Include(m => m.Tasks)
            .FirstOrDefaultAsync(m => m.Id == id);
       

        public async Task<List<Meeting>> GetByProject(int projectId)
        
        => await _db.Meetings
            .Include(m => m.Participants)
            .Include(m => m.AgendaItems)
            .Include(m => m.Tasks)
            .ToListAsync();

   

        public async Task<Meeting> Update(int id, Meeting updated)
        {
            var existing = await _db.Meetings.FindAsync(id);
            if (existing == null) throw new Exception("Meeting not found");

            existing.Title = updated.Title;
            existing.Location = updated.Location;
            existing.StartTime = updated.StartTime;
            existing.EndTime = updated.EndTime;
            existing.MeetingMinutes = updated.MeetingMinutes;

            await _db.SaveChangesAsync();
            return existing;
        }
    }
}
