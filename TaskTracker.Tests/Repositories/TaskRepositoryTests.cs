using TaskTracker.CLI.Data;
using TaskTracker.CLI.Models;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

namespace TaskTracker.Tests.Repositories;

public class TaskRepositoryTests
{
    [Fact]
    public void AddTask_ShouldAssignIncrementalId()
    {
        TaskRepository.ResetIdCounter();
        var repo = new TaskRepository();
        
        var task1 = new TaskItem { Description = "Task 1" };
        var task2 = new TaskItem { Description = "Task 2" };
        
        repo.AddTask(task1);
        repo.AddTask(task2);
        
        Assert.Equal(1, task1.Id);
        Assert.Equal(2, task2.Id);
    }

    [Fact]
    public void UpdateTaskDescription_ShouldChangeDescription()
    {
        var repo = new TaskRepository();
        var task1 = new TaskItem { Description = "Task 1" };
        
        repo.AddTask(task1);
        repo.UpdateTaskDescription(task1.Id, "New Description1");
        
        Assert.Equal("New Description1", task1.Description);
    }
    
    [Fact]
    public void UpdateTaskStatus_ShouldChangeStatus()
    {
        var repo = new TaskRepository();
        var task1 = new TaskItem { Description = "Task 1" };
        var task2 = new TaskItem { Description = "Task 2" };
        
        repo.AddTask(task1);
        repo.AddTask(task2);
        repo.UpdateTaskStatus(task1.Id, TaskStatus.InProgress);
        repo.UpdateTaskStatus(task2.Id, TaskStatus.Done);
        
        Assert.Equal(TaskStatus.InProgress, task1.Status);
        Assert.Equal(TaskStatus.Done, task2.Status);
    }
    
    [Fact]
    public void DeleteTask_ShouldRemoveSpecificTask()
    {
        var repo = new TaskRepository();
        var task1 = new TaskItem { Description = "Task 1" };
        var task2 = new TaskItem { Description = "Task 2" };
        
        repo.AddTask(task1);
        repo.AddTask(task2);
        
        repo.DeleteTask(task1.Id);
        
        Assert.Null(repo.GetTaskById(task1.Id));
        Assert.Single(repo.GetAllTasks());
        Assert.Equal(task2.Id, repo.GetAllTasks().First().Id);
    }
    
    [Fact]
    public void ClearAllTasks_ShouldRemoveAllTasks()
    {
        var repo = new TaskRepository();
        var task1 = new TaskItem { Description = "Task 1" };
        var task2 = new TaskItem { Description = "Task 2" };
        
        repo.AddTask(task1);
        repo.AddTask(task2);
        
        repo.ClearAllTasks();
        
        Assert.Empty(repo.GetAllTasks());
    }
    
    [Fact]
    public void PrintAllTasks_ShouldPrintAllTasks()
    {
        var repo = new TaskRepository();
        var task1 = new TaskItem { Description = "Task 1" };
        var task2 = new TaskItem { Description = "Task 2" };
        
        repo.AddTask(task1);
        repo.AddTask(task2);
        
        repo.PrintAllTasks();
        
        Assert.Equal(2, repo.GetAllTasks().Count);
    }
    
}