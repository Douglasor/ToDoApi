
namespace ToDoApi.Application.Interfaces;

using ToDoApi.Application.DTOs;
using ToDoApi.Domain.Entities;
using ToDoApi.Domain.Enun;

public interface ITaskService
{
    TaskDto GetTaskById(Guid id);
    IEnumerable<TaskDto> GetAllTasks();
    IEnumerable<TaskDto> GetTasksByStatusAndDueDate(Status? status, DateTime? dueDate);
    IEnumerable<TaskDto> GetTasksByStatus(Status status);
    IEnumerable<TaskDto> GetTasksByDueDate(DateTime dueDate);
    TaskDto CreateTask(TaskDto taskDto);
    void UpdateTask(TaskDto taskDto);
    void DeleteTask(Guid id);
}

