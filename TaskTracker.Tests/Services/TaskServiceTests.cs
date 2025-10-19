using Moq;
using TaskTracker.CLI.Interfaces;
using TaskTracker.CLI.Models;
using TaskTracker.CLI.Services;

namespace TaskTracker.Tests.Services;

public class TaskServiceTests
{
    /*
     ToDo: 
     GetTaskById_ReturnsNull_WhenTaskDoesNotExist
     UpdateTaskStatus_ThrowsException_WhenStatusIsSame
     DeleteTask_RemovesSuccessfully_WhenTaskExists
     ClearAllTasks_ThrowsException_WhenTasksAreEmpty
     */

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
}