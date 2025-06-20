namespace ToDoApi.Infrastructure.UnitOfWork;

using ToDoApi.Domain.Interfaces;
using ToDoApi.Infrastructure.Data;
using ToDoApi.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ITaskRepository _taskRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ITaskRepository Tasks
    {
        get
        {
            if (_taskRepository == null)
            {
                _taskRepository = new TaskRepository(_context);
            }
            return _taskRepository;
        }
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

