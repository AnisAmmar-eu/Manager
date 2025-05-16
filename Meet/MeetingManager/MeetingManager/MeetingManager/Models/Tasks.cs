namespace MeetingManager.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public StatusType Status { get; set; } 
        public DateTime? DueDate { get; set; }
        public TypeTask TypeTask { get; set; }

        public Priority Priority { get; set; }
        public int? MeetingId { get; set; }
        public Meeting? Meeting { get; set; }

    }

    public enum Priority
    {
        High,
        Medium,
        Low
    }

    public enum TypeTask
    {
        Requirments,
        Notes
    }
}