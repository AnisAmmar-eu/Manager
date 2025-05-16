namespace MeetingManager.Models
{
    public class Agenda
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime Duration { get; set; }

        public DateTime FinishTime {  get; set; }

        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
