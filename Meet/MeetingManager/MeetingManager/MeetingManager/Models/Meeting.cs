using System.Runtime.InteropServices;

namespace MeetingManager.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }

        public string? Link { get; set; }

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

        public ICollection<Participant>? Participants { get; set; }
        public ICollection<Agenda>? AgendaItems { get; set; }
        public ICollection<Tasks>? Tasks { get; set; }

        public string? MeetingMinutes { get; set; }
        
    }
}
