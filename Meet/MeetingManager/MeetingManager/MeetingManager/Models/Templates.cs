namespace MeetingManager.Models
{
    public class Templates
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? MeetingId { get; set; }
        public Meeting? Meeting { get; set; }
    }
}
