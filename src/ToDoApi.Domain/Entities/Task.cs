using ToDoApi.Domain.Enun;

namespace ToDoApi.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }
}

