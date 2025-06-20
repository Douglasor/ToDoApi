namespace ToDoApi.UnitTests;

using Moq;
using Xunit;
using ToDoApi.Application.DTOs;
using ToDoApi.Application.Services;
using ToDoApi.Domain.Interfaces;
using ToDoApi.Domain.Enun;

public class TaskServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _taskService = new TaskService(_mockUnitOfWork.Object);
    }

    [Fact]
    public void GetTaskById_ShouldReturnTaskDto_WhenTaskExists()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = new ToDoApi.Domain.Entities.Task { Id = taskId, Title = "Test Task", Description = "Description", Status = Status.Pendente, DueDate = DateTime.Now };
        _mockUnitOfWork.Setup(uow => uow.Tasks.GetById(taskId)).Returns(task);

        // Act
        var result = _taskService.GetTaskById(taskId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskId, result.Id);
        Assert.Equal("Test Task", result.Title);
    }

    [Fact]
    public void GetTaskById_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        _mockUnitOfWork.Setup(uow => uow.Tasks.GetById(taskId)).Returns((ToDoApi.Domain.Entities.Task)null);

        // Act
        var result = _taskService.GetTaskById(taskId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CreateTask_ShouldAddTaskAndCompleteUnitOfWork()
    {
        // Arrange
        var taskDto = new TaskDto { Title = "New Task", Description = "New Description", Status = Status.Pendente, DueDate = DateTime.Now };
        _mockUnitOfWork.Setup(uow => uow.Tasks.Add(It.IsAny<ToDoApi.Domain.Entities.Task>()));
        _mockUnitOfWork.Setup(uow => uow.Complete()).Returns(1);

        // Act
        var result = _taskService.CreateTask(taskDto);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.Tasks.Add(It.IsAny<ToDoApi.Domain.Entities.Task>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
        Assert.NotNull(result.Id);
        Assert.Equal("New Task", result.Title);
    }

    [Fact]
    public void UpdateTask_ShouldUpdateTaskAndCompleteUnitOfWork_WhenTaskExists()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var existingTask = new ToDoApi.Domain.Entities.Task { Id = taskId, Title = "Old Title", Description = "Old Description", Status = Status.Pendente, DueDate = DateTime.Now };
        var updatedTaskDto = new TaskDto { Id = taskId, Title = "Updated Title", Description = "Updated Description", Status = Status.Concluido, DueDate = DateTime.Now.AddDays(1) };

        _mockUnitOfWork.Setup(uow => uow.Tasks.GetById(taskId)).Returns(existingTask);
        _mockUnitOfWork.Setup(uow => uow.Tasks.Update(It.IsAny<ToDoApi.Domain.Entities.Task>()));
        _mockUnitOfWork.Setup(uow => uow.Complete()).Returns(1);

        // Act
        _taskService.UpdateTask(updatedTaskDto);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.Tasks.Update(existingTask), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
        Assert.Equal("Updated Title", existingTask.Title);
        Assert.Equal(Status.Concluido, existingTask.Status);
    }

    [Fact]
    public void UpdateTask_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var updatedTaskDto = new TaskDto { Id = taskId, Title = "Updated Title", Description = "Updated Description", Status = Status.Concluido, DueDate = DateTime.Now.AddDays(1) };

        _mockUnitOfWork.Setup(uow => uow.Tasks.GetById(taskId)).Returns((ToDoApi.Domain.Entities.Task)null);

        // Act & Assert
        var exception = Assert.Throws<KeyNotFoundException>(() => _taskService.UpdateTask(updatedTaskDto));
        Assert.Equal($"Task with ID {taskId} not found.", exception.Message);
        _mockUnitOfWork.Verify(uow => uow.Tasks.Update(It.IsAny<ToDoApi.Domain.Entities.Task>()), Times.Never);
        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Never);
    }

    [Fact]
    public void DeleteTask_ShouldDeleteTaskAndCompleteUnitOfWork_WhenTaskExists()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var taskToDelete = new ToDoApi.Domain.Entities.Task { Id = taskId, Title = "Task to Delete", Description = "Description", Status = Status.Pendente, DueDate = DateTime.Now };

        _mockUnitOfWork.Setup(uow => uow.Tasks.GetById(taskId)).Returns(taskToDelete);
        _mockUnitOfWork.Setup(uow => uow.Tasks.Delete(It.IsAny<ToDoApi.Domain.Entities.Task>()));
        _mockUnitOfWork.Setup(uow => uow.Complete()).Returns(1);

        // Act
        _taskService.DeleteTask(taskId);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.Tasks.Delete(taskToDelete), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }

    [Fact]
    public void DeleteTask_ShouldDoNothing_WhenTaskDoesNotExist()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        _mockUnitOfWork.Setup(uow => uow.Tasks.GetById(taskId)).Returns((ToDoApi.Domain.Entities.Task)null);

        // Act
        _taskService.DeleteTask(taskId);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.Tasks.Delete(It.IsAny<ToDoApi.Domain.Entities.Task>()), Times.Never);
        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Never);
    }
}

