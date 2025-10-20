using Moq;
using TaskTracker.CLI.Interfaces;
using TaskTracker.CLI.Models;
using TaskTracker.CLI.Services;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

namespace TaskTracker.Tests.Services;

public class TaskServiceTests
{
    private readonly TaskService _service;

    public TaskServiceTests()
    {
        var mockRepo = new Mock<ITaskRepository>();
        _service = new TaskService(mockRepo.Object);
    }

    [Fact]
    public void AddTask_ThrowsException_WhenDescriptionIsEmpty()
    {
        const string description = " ";
        
        Assert.Throws<ArgumentException>(() => _service.AddTask(description));
    }
    
    [Fact]
    public void AddTask_ThrowsException_WhenTaskAlreadyExists()
    {
        var mockRepo = new Mock<ITaskRepository>();
        mockRepo.Setup(r => r.GetAllTasks()).Returns([new TaskItem { Description = "Task 1" }]);
        
        var service = new TaskService(mockRepo.Object);
        
        Assert.Throws<InvalidOperationException>(() => service.AddTask("Task 1"));
    }

    [Fact]
    public void GetTaskById_ReturnsNull_WhenTaskDoesNotExist()
    {
        var mockRepo = new Mock<ITaskRepository>();
        
        var service = new TaskService(mockRepo.Object);
        
        Assert.Null(service.GetTaskById(1));   
    }

    [Fact]
    public void UpdateTaskStatus_ThrowsException_WhenStatusIsSame()
    {
        var task = new TaskItem { Description = "Task 1", Status = TaskStatus.InProgress};
        
        var mockRepo = new Mock<ITaskRepository>();
        mockRepo.Setup(r => r.GetTaskById(1)).Returns(task);
        
        var service = new TaskService(mockRepo.Object);
        
        Assert.Throws<InvalidOperationException>(() => service.UpdateTaskStatus(1, TaskStatus.InProgress));   
    }

    [Fact]
    public void DeleteTask_RemovesSuccessfully_WhenTaskExists()
    {
        var mockRepo = new Mock<ITaskRepository>();
        var task = new TaskItem { Description = "Task 1" };
        mockRepo.Setup(r => r.GetTaskById(1)).Returns(task);
        
        var service = new TaskService(mockRepo.Object);
        
        Assert.True(service.DeleteTask(1));  
    }

    [Fact]
    public void ClearAllTasks_ThrowsException_WhenTasksAreEmpty()
    {
        var mockRepo = new Mock<ITaskRepository>();
        mockRepo.Setup(r => r.GetAllTasks()).Returns([]);
        
        var service = new TaskService(mockRepo.Object);
        
        Assert.Throws<InvalidOperationException>(() => service.ClearAllTasks());  
    }
}