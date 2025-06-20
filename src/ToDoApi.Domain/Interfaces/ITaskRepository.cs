
namespace ToDoApi.Domain.Interfaces;

using ToDoApi.Domain.Entities;
using ToDoApi.Domain.Enun;

public interface ITaskRepository
{
    Task GetById(Guid id);
    IEnumerable<Task> GetAll();
    IEnumerable<Task> GetByStatusAndDueDate(Status? status, DateTime? dueDate);
    IEnumerable<Task> GetByStatus(Status status);
    IEnumerable<Task> GetByDueDate(DateTime dueDate);
    void Add(Task task);
    void Update(Task task);
    void Delete(Task task);
}

