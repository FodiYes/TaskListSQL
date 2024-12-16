using System;

namespace TaskList.Models
{
    public class TimeTracking
    {
        public int TrackingId { get; set; }
        public int TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TodoTask Task { get; set; } = null!;
    }
}
