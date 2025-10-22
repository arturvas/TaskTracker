using TaskTracker.CLI.Interfaces;
using TaskTracker.CLI.Models;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

namespace TaskTracker.CLI.Services;

public class TaskService(ITaskRepository repository) : ITaskService
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
    
    public TaskItem AddTask(string description)
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
        return task;
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
        ValidateId(id);
        return repository.GetTaskById(id)!;
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
            throw new InvalidOperationException("Tasks are already empty");
        
        repository.ClearAllTasks();
        return true;
    }

    public void PrintTaskById(int id)
    {
        var task = EnsureTaskExists(id);
        
        Console.WriteLine(task.Description);
    }

    public void PrintTasksByStatus(TaskStatus status)
    {
        var task = repository
            .GetAllTasks()
            .Where(t => t.Status == status);
        
        foreach (var taskItem in task)
            Console.WriteLine(taskItem.Description);
    }

    public TaskItem UpdateTaskStatus(int id, TaskStatus status)
    {
        var task = EnsureTaskExists(id);
        
        if (task.Status == status)
            throw new InvalidOperationException($"Task ({task.Description}) is already in {status} status");
        
        task.Status = status;
        task.UpdatedAt = DateTime.UtcNow;
        return task;
    }

    public TaskItem UpdateTaskDescription(int id, string description)
    {
        var task = EnsureTaskExists(id);
        
        if (task.Description.Equals(description, StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidOperationException($"Task ({task.Description}) already has the same description");
        
        task.Description = description;
        task.UpdatedAt = DateTime.UtcNow;
        return task;
    }
    
    public List<string> GetHelpCommands()
    {
        return
        [
            "\tadd <description> \t To add a new task, type add with task description",
            "\tupdate <id> <description> To update a task description by id",
            "\tdelete <id> \t\t To delete a task by id",
            "\tmark-in-progress <id> \t To mark a task as in progress by id",
            "\tmark-done <id> \t\t To mark a task as done by id",
            "\tlist \t\t\t To list all tasks",
            "\tlist done \t\t To list all done tasks",
            "\tlist todo \t\t To list all pending tasks",
            "\tlist in-progress \t To list all in-progress tasks",
            "\tclear \t\t\t To clear all tasks",
            "\tprint <id> \t\t To print a task by id"
        ];
    }
}