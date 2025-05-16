namespace MeetingManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<Meeting> Meetings { get; set; }

        public ICollection<Participant> Participants { get; set; }

    }
}
