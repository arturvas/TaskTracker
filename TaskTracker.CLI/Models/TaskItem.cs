namespace TaskTracker.CLI.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public override string ToString() => $"[{Id}] {Description} - {Status}";
}