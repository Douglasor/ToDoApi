namespace ToDoApi.Application.Services;

using ToDoApi.Application.DTOs;
using ToDoApi.Application.Interfaces;
using ToDoApi.Domain.Entities;
using ToDoApi.Domain.Enun;
using ToDoApi.Domain.Interfaces;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public TaskDto GetTaskById(Guid id)
    {
        var task = _unitOfWork.Tasks.GetById(id);
        return task == null ? null : MapToDto(task);
    }

    public IEnumerable<TaskDto> GetAllTasks()
    {
        return _unitOfWork.Tasks.GetAll().Select(MapToDto);
    }

    public IEnumerable<TaskDto> GetTasksByStatusAndDueDate(Status? status, DateTime? dueDate)
    {
        return _unitOfWork.Tasks.GetByStatusAndDueDate(status, dueDate).Select(MapToDto);
    }

    public IEnumerable<TaskDto> GetTasksByStatus(Status status)
    {
        return _unitOfWork.Tasks.GetByStatus(status).Select(MapToDto);
    }

    public IEnumerable<TaskDto> GetTasksByDueDate(DateTime dueDate)
    {
        return _unitOfWork.Tasks.GetByDueDate(dueDate).Select(MapToDto);
    }

    public TaskDto CreateTask(TaskDto taskDto)
    {
        var task = MapToEntity(taskDto);
        task.Id = Guid.NewGuid(); // Generate new ID for new task
        _unitOfWork.Tasks.Add(task);
        _unitOfWork.Complete();
        return MapToDto(task);
    }

    public void UpdateTask(TaskDto taskDto)
    {
        var existingTask = _unitOfWork.Tasks.GetById(taskDto.Id);
        if (existingTask == null)
        {
            throw new KeyNotFoundException($"Task with ID {taskDto.Id} not found.");
        }

        existingTask.Title = taskDto.Title;
        existingTask.Description = taskDto.Description;
        existingTask.Status = taskDto.Status;
        existingTask.DueDate = taskDto.DueDate;

        _unitOfWork.Tasks.Update(existingTask);
        _unitOfWork.Complete();
    }

    public void DeleteTask(Guid id)
    {
        var task = _unitOfWork.Tasks.GetById(id);
        if (task != null)
        {
            _unitOfWork.Tasks.Delete(task);
            _unitOfWork.Complete();
        }
    }

    private TaskDto MapToDto(Task task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            DueDate = task.DueDate
        };
    }

    private Task MapToEntity(TaskDto taskDto)
    {
        return new Task
        {
            Id = taskDto.Id,
            Title = taskDto.Title,
            Description = taskDto.Description,
            Status = taskDto.Status,
            DueDate = taskDto.DueDate
        };
    }
}

