using TaskTracker.CLI.Models;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

namespace TaskTracker.CLI.Data;

public class TaskRepository
{
    private static int _nextId = 1;
    private readonly List<TaskItem> _task = [];

    public void AddTask(TaskItem task)
    {
        task.Id = _nextId++;
        _task.Add(task);
    }
    
    public List<TaskItem> GetAllTasks()
    {
        return _task;
    }

    private TaskItem? GetTaskById(int id)
    {
        return _task.FirstOrDefault(t => t.Id == id);
    }

    public void UpdateTask(TaskItem task)
    {
        var existingTask = GetTaskById(task.Id);
        if (existingTask == null) return;
        existingTask.Description = task.Description;
        existingTask.Status = task.Status;
        existingTask.UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateTaskStatus(int id, TaskStatus status)
    {
        var existingTask = GetTaskById(id);
        if (existingTask == null) return;
        existingTask.Status = status;
        existingTask.UpdatedAt = DateTime.UtcNow;   
    }

    public void UpdateTaskDescription(int id, string description)
    {
        var existingTask = GetTaskById(id);
        if (existingTask == null) return;
        existingTask.Description = description;
        existingTask.UpdatedAt = DateTime.UtcNow;
    }
    
    public void DeleteTask(int id)
    {
        var existingTask = GetTaskById(id);
        if (existingTask == null) return;
        _task.Remove(existingTask);
    }
    
    public void ClearAllTasks()
    {
        _task.Clear();
    }
    
    public void PrintAllTasks()
    {
        foreach (var task in _task)
        {
            Console.WriteLine(task);
        }
    }

    public void PrintTaskById(int id)
    {
        var task = GetTaskById(id);
        if (task == null) return;
        Console.WriteLine(task);   
    }

    public void PrintTaskByStatus(TaskStatus status)
    {
        var tasks = _task.Where(t => t.Status == status);
    }
    
    public void PrintTaskByDescription(string description)
    {
        var tasks = _task.Where(t => t.Description.Contains(description));
    }

}