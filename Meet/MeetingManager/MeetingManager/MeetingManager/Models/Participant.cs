using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MeetingManager.Models
{
    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }

        public int? MeetingId { get; set; }

        public int? ProjectId { get; set; }
        public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();

    }

}