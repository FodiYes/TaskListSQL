using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
    public enum TaskPriority
    {
        [Display(Name = "Low")]
        Low,
        [Display(Name = "Normal")]
        Normal,
        [Display(Name = "High")]
        High,
        [Display(Name = "Urgent")]
        Urgent
    }

    public static class TaskPriorityExtensions
    {
        public static TaskPriority[] Values => (TaskPriority[])Enum.GetValues(typeof(TaskPriority));
    }

    public class TodoTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; } = DateTime.Now.AddDays(1);
        public TaskPriority Priority { get; set; } = TaskPriority.Normal;
        public TodoTaskStatus Status { get; set; } = TodoTaskStatus.NotStarted;
        public TimeSpan TotalTime { get; set; } = TimeSpan.Zero;
        public TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;
        public ICollection<TimeTracking> TimeTrackings { get; set; } = new List<TimeTracking>();

        public TimeSpan CalculateTotalTime()
        {
            TimeSpan total = TimeSpan.Zero;
            foreach (var tracking in TimeTrackings)
            {
                if (tracking.EndTime.HasValue)
                {
                    total += tracking.EndTime.Value - tracking.StartTime;
                }
            }
            return total;
        }
    }
}
