using TaskTracker.CLI.Data;
using TaskTracker.CLI.Models;

namespace TaskTracker.CLI.Services;

public class TaskService(TaskRepository repository)
{
    // regras de negócio: validações, transições de status, timestamps, etc.
    public void AddTask(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty");
        
        var exists = repository
            .GetAllTasks()
            .Any(t => t.Description.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        
        if (exists)
            throw new InvalidOperationException("Task already exists");

        var task = new TaskItem
        {
            Description = description.Trim(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        repository.AddTask(task);
    }

    public void GetTaskById(int id)
    {
        var task = repository.GetTaskById(id);
        
        if (id <= 0)
            throw new ArgumentException("Id must be greater than 0");
        
        if (task == null)
            throw new InvalidOperationException("Task not found");
        
        Console.WriteLine(task);
    }
}