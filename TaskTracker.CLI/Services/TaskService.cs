using TaskTracker.CLI.Data;
using TaskTracker.CLI.Interfaces;
using TaskTracker.CLI.Models;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

namespace TaskTracker.CLI.Services;

public class TaskService(TaskRepository repository) : ITaskService
{
    private static void ValidateId(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be greater than 0");
    }

    private TaskItem EnsureTaskExists(int id)
    {
        ValidateId(id);
        
        var task = repository.GetTaskById(id);
        
        return task ?? throw new InvalidOperationException("Task not found");
    }
    
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

    public List<TaskItem> GetAllTasks()
    {
        var tasks = repository.GetAllTasks();
        
        return tasks.Count == 0 ? throw new InvalidOperationException("No tasks found") : tasks;
    }

    public void PrintAllTasks()
    {
        var tasks = repository.GetAllTasks();
        
        if (tasks.Count == 0)
            throw new InvalidOperationException("No tasks found");
        
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    public TaskItem GetTaskById(int id)
    {
        return EnsureTaskExists(id);
    }

    public bool DeleteTask(int id)
    {
        EnsureTaskExists(id);
        repository.DeleteTask(id);
        return true;
    }

    public bool ClearAllTasks()
    {
        var tasks = repository.GetAllTasks();
        
        if (tasks.Count == 0)
            throw new InvalidOperationException("No tasks found");
        
        repository.ClearAllTasks();
        return true;
    }

    public bool PrintTaskById(int id)
    {
        var task = EnsureTaskExists(id);
        
        Console.WriteLine(task.Description);
        return true;
    }

    public bool PrintTasksByStatus(TaskStatus status)
    {
        var task = repository
            .GetAllTasks()
            .Where(t => t.Status == status);
        
        foreach (var taskItem in task)
            Console.WriteLine(taskItem.Description);
        return true;
    }

    public void UpdateTaskStatus(int id, TaskStatus status)
    {
        var task = EnsureTaskExists(id);
        
        if (task.Status == status)
            throw new InvalidOperationException($"Task is already in {status} status");
        
        task.Status = status;
        task.UpdatedAt = DateTime.UtcNow;
    }

    public bool UpdateTaskDescription(int id, string description)
    {
        var task = EnsureTaskExists(id);
        
        task.Description = description;
        task.UpdatedAt = DateTime.UtcNow;
        return true;
    }
    
    public List<string> GetHelpCommands()
    {
        return
        [
            "add <description> - To add a new task, type add with task description",
            "update <id> <description> - To update a task description by id",
            "delete <id> - To delete a task by id",
            "mark-in-progress <id> - To mark a task as in progress by id",
            "mark-done <id> - To mark a task as done by id",
            "list - To list all tasks",
            "list done - To list all done tasks",
            "list todo - To list all pending tasks",
            "list in-progress - To list all in-progress tasks",
            "clear - To clear all tasks",
            "print <id> - To print a task by id"
        ];
    }
}