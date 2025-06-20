namespace ToDoApi.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoApi.Application.DTOs;
using ToDoApi.Application.Interfaces;
using ToDoApi.Domain.Enun;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TaskDto>> GetAllTasks()
    {
        var tasks = _taskService.GetAllTasks();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskDto> GetTaskById(Guid id)
    {
        var task = _taskService.GetTaskById(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpGet("by-status-and-due-date")]
    public ActionResult<IEnumerable<TaskDto>> GetTasks([FromQuery] Status? status, [FromQuery] DateTime? dueDate)
    {
        var tasks = _taskService.GetTasksByStatusAndDueDate(status, dueDate);
        return Ok(tasks);
    }


    [HttpGet("by-status/{status}")]
    public ActionResult<IEnumerable<TaskDto>> GetTasksByStatus(Status status)
    {
        var tasks = _taskService.GetTasksByStatus(status);
        return Ok(tasks);
    }

    [HttpGet("by-due-date/{dueDate}")]
    public ActionResult<IEnumerable<TaskDto>> GetTasksByDueDate(DateTime dueDate)
    {
        var tasks = _taskService.GetTasksByDueDate(dueDate);
        return Ok(tasks);
    }

    [HttpPost]
    public ActionResult<TaskDto> CreateTask([FromBody] TaskDto taskDto)
    {
        var createdTask = _taskService.CreateTask(taskDto);
        return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(Guid id, [FromBody] TaskDto taskDto)
    {
        if (id != taskDto.Id)
        {
            return BadRequest("ID mismatch");
        }
        try
        {
            _taskService.UpdateTask(taskDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(Guid id)
    {
        _taskService.DeleteTask(id);
        return NoContent();
    }
}

