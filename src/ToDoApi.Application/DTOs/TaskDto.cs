namespace ToDoApi.Application.DTOs;

using ToDoApi.Domain.Enun;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }
}

