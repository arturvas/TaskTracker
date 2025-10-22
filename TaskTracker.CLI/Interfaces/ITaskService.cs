using TaskTracker.CLI.Models;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

namespace TaskTracker.CLI.Interfaces;

public interface ITaskService
{
    List<TaskItem> GetAllTasks();
    TaskItem GetTaskById(int id);
    TaskItem AddTask(string description);
    bool DeleteTask(int id);
    bool ClearAllTasks();
    void PrintTaskById(int id);
    void PrintTasksByStatus(TaskStatus status);
    void PrintAllTasks();
    TaskItem UpdateTaskStatus(int id, TaskStatus status);
    TaskItem UpdateTaskDescription(int id, string description);
    List<string> GetHelpCommands();
}