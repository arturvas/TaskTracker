using TaskTracker.CLI.Models;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

namespace TaskTracker.CLI.Interfaces;

public interface ITaskService
{
    List<TaskItem> GetAllTasks();
    TaskItem GetTaskById(int id);
    void AddTask(string description);
    bool DeleteTask(int id);
    bool ClearAllTasks();
    bool PrintTaskById(int id);
    bool PrintTasksByStatus(TaskStatus status);
    void PrintAllTasks();
    void UpdateTaskStatus(int id, TaskStatus status);
    bool UpdateTaskDescription(int id, string description);
    List<string> GetHelpCommands();
}