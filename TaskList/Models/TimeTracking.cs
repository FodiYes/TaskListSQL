using System;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
    public class TimeTracking
    {
        [Key]
        public int TrackingId { get; set; }
        public int TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TodoTask Task { get; set; } = null!;
    }
}
