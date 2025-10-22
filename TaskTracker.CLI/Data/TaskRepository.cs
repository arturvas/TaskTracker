using System.Text.Json;
using TaskTracker.CLI.Interfaces;
using TaskTracker.CLI.Models;
using static TaskTracker.CLI.Utils.JsonUtils;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

namespace TaskTracker.CLI.Data;

public class TaskRepository : ITaskRepository
{
    private readonly string _filePath;
    private static int _nextId = 1;
    private List<TaskItem> _task = [];
    public static void ResetIdCounter() => _nextId = 1;
    
    private const string DefaultFileName = "tasks_data.json";
    private static readonly string DefaultFilePath =
        Path.Combine(Directory.GetCurrentDirectory(), DefaultFileName);

    public TaskRepository() : this(DefaultFilePath) { }

    public TaskRepository(string filePath)
    {
        _filePath = filePath;
        LoadFromFile();
    }

    private void LoadFromFile()
    {
        if (!File.Exists(_filePath))
        {
            _task = [];
            SaveToFile();
            return;
        }
        var json = File.ReadAllText(_filePath);
        _task = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? [];
    }
    
    private void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_task, GetJsonOptions());
        
        File.WriteAllText(_filePath, json);
    }


    public bool AddTask(TaskItem task)
    {
        task.Id = _nextId++;
        _task.Add(task);
        SaveToFile();
        return true;
    }
    
    public List<TaskItem> GetAllTasks() => _task;

    public TaskItem? GetTaskById(int id) => _task.FirstOrDefault(t => t.Id == id);

    public bool UpdateTaskStatus(int id, TaskStatus status)
    {
        var existingTask = GetTaskById(id);
        
        if (existingTask == null) return false;
        existingTask.Status = status;
        existingTask.UpdatedAt = DateTime.UtcNow;  
        return true;
    }

    public bool UpdateTaskDescription(int id, string description)
    {
        var existingTask = GetTaskById(id);
        
        if (existingTask == null) return false;
        existingTask.Description = description;
        existingTask.UpdatedAt = DateTime.UtcNow;
        return true;
    }
    
    public bool DeleteTask(int id)
    {
        var existingTask = GetTaskById(id);
        
        if (existingTask == null) return false;
        _task.Remove(existingTask);
        return true;
    }
    
    public bool ClearAllTasks()
    {
        _task.Clear();
        return true;
    }
    
    public bool PrintAllTasks()
    {
        foreach (var task in _task)
        {
            Console.WriteLine(task);
        }
        return true;
    }

    public bool PrintTaskById(int id)
    {
        var task = GetTaskById(id);
        
        if (task == null) return false;
        Console.WriteLine(task);  
        return true;
    }

    public bool PrintTasksByStatus(TaskStatus status)
    {
        var tasks = _task.Where(t => t.Status == status).ToList();
        
        if (tasks.Count == 0) return false;
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
        return true;
    }
    
    public void PrintTaskByDescription(string description)
    {
        var taskItems = _task.Where(t => t.Description.Contains(description));
    }
}