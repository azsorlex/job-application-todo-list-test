using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoListApi.Application.Services.IServices;
using TodoListApi.Domain.Models;

namespace TodoListApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TodosController : ControllerBase
{
    private readonly ITodosService _service;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodosService service, ILogger<TodosController> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public IEnumerable<Todo> GetTodos()
    {
       _logger.LogInformation("Fetching all todos");

        var todos = _service.GetTodos();

        _logger.LogInformation($"Found {todos.Count} todos");

        return todos;
    }
}