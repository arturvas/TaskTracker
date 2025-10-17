using TaskTracker.CLI.Models;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

namespace TaskTracker.CLI.Interfaces;

public interface ITaskRepository
{
    bool AddTask(TaskItem task);
    List<TaskItem> GetAllTasks();
    TaskItem? GetTaskById(int id);
    bool UpdateTaskStatus(int id, TaskStatus status);
    bool UpdateTaskDescription(int id, string description);
    bool DeleteTask(int id);
    bool ClearAllTasks();
    bool PrintAllTasks();
    bool PrintTaskById(int id);
    bool PrintTasksByStatus(TaskStatus status);
    void PrintTaskByDescription(string description);
}