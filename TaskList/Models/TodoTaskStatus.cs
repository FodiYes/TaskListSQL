namespace TaskList.Models
{
    public enum TodoTaskStatus
    {
        NotStarted,
        InProgress,
        Completed,
        OnHold
    }

    public static class TodoTaskStatusExtensions
    {
        public static TodoTaskStatus[] Values => (TodoTaskStatus[])Enum.GetValues(typeof(TodoTaskStatus));
    }
}
