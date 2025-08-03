using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using TodoListApi.Application.Services.IServices;
using TodoListApi.Domain.Models;

namespace TodoListApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class TodosController : ControllerBase
{
    private readonly ITodosService _service;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodosService service, ILogger<TodosController> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public ActionResult<IEnumerable<Todo>> GetTodos()
    {
       _logger.LogInformation("Fetching all todos");
        var todos = _service.GetTodos();
        _logger.LogInformation($"Found {todos.Count} todos");

        return todos;
    }

    [HttpPost("add")]
    public IActionResult AddTodo([FromBody, Required] Todo todo)
    {
        if (todo == null)
        {
            _logger.LogWarning("Received null todo");
            return BadRequest("Todo cannot be null");
        }

        _logger.LogInformation($"Adding todo: {todo.Name}");
        _service.AddTodo(todo);
        _logger.LogInformation($"Todo '{todo.Name}' added successfully");

        return CreatedAtAction(nameof(GetTodos), new { success = true });
    }

    [HttpPatch("toggle/{id}")]
    public IActionResult ToggleTodoCompletion(Guid id)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Received empty GUID for todo toggle");
            return BadRequest("Invalid todo ID");
        }
        _logger.LogInformation($"Toggling completion status for todo with ID: {id}");
        _service.ToggleTodoCompletion(id);
        _logger.LogInformation($"Todo with ID '{id}' toggled successfully");
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteTodo(Guid id)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Received empty GUID for todo deletion");
            return BadRequest("Invalid todo ID");
        }
        _logger.LogInformation($"Deleting todo with ID: {id}");
        _service.DeleteTodo(id);
        _logger.LogInformation($"Todo with ID '{id}' deleted successfully");
        return NoContent();
    }
}