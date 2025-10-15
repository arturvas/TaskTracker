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
        
        repo.AddTask(task1);
        repo.UpdateTaskStatus(task1.Id, TaskStatus.InProgress);
        
        Assert.Equal(TaskStatus.InProgress, task1.Status);
    }
    
}