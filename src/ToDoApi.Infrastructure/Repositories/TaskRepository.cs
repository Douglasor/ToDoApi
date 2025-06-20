
namespace ToDoApi.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using ToDoApi.Domain.Entities;
using ToDoApi.Domain.Enun;
using ToDoApi.Domain.Interfaces;
using ToDoApi.Infrastructure.Data;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task GetById(Guid id)
    {
        return _context.Tasks.Find(id);
    }

    public IEnumerable<Task> GetAll()
    {
        return _context.Tasks.ToList();
    }

    public IEnumerable<Task> GetByStatusAndDueDate(Status? status, DateTime? dueDate)
    {
        var query = _context.Tasks.AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(t => t.Status == status.Value);
        }

        if (dueDate.HasValue)
        {
            query = query.Where(t => t.DueDate.Date == dueDate.Value.Date);
        }

        return query.ToList();
    }


    public IEnumerable<Task> GetByStatus(Status status)
    {
        return _context.Tasks.Where(t => t.Status == status).ToList();
    }

    public IEnumerable<Task> GetByDueDate(DateTime dueDate)
    {
        return _context.Tasks.Where(t => t.DueDate.Date == dueDate.Date).ToList();
    }

    public void Add(Task task)
    {
        _context.Tasks.Add(task);
    }

    public void Update(Task task)
    {
        _context.Tasks.Update(task);
    }

    public void Delete(Task task)
    {
        _context.Tasks.Remove(task);
    }
}

