namespace MeetingManager.Models
{
        public class Project
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string? Description { get; set; }
            public DateTime CreatedAt { get; set; }

            public int? OwnerId { get; set; }
            public User? Owner { get; set; }

            public ICollection<Meeting>? Meetings { get; set; }

            public List<Participant> Participants { get; set; }
        }

}
