using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TodoListApi.Application.Services.IServices;
using TodoListApi.Domain.Models;
using TodoListApi.Presentation.Controllers;

namespace TodoListApi.Tests.Presentation.Controllers;

public class TodosControllerTests
{
    private readonly Mock<ITodosService> _serviceMock;
    private readonly Mock<ILogger<TodosController>> _loggerMock;
    private readonly TodosController _controller;

    public TodosControllerTests()
    {
        _serviceMock = new Mock<ITodosService>();
        _loggerMock = new Mock<ILogger<TodosController>>();
        _controller = new TodosController(_serviceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void GetTodos_ReturnsAllTodos()
    {
        // Arrange
        var todos = new List<Todo>
            {
                new Todo { Id = Guid.NewGuid(), Name = "Test 1", IsCompleted = false },
                new Todo { Id = Guid.NewGuid(), Name = "Test 2", IsCompleted = true }
            };
        _serviceMock.Setup(s => s.GetTodos()).Returns(todos);

        // Act
        var result = _controller.GetTodos();

        // Assert
        var okResult = Assert.IsType<ActionResult<IEnumerable<Todo>>>(result);
        Assert.Equal(todos, okResult.Value);
        _serviceMock.Verify(s => s.GetTodos(), Times.Once);
    }

    [Fact]
    public void AddTodo_NullTodo_ReturnsBadRequest()
    {
        // Act
        var result = _controller.AddTodo(null);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Todo cannot be null", badRequest.Value);
        _serviceMock.Verify(s => s.AddTodo(It.IsAny<Todo>()), Times.Never);
    }

    [Fact]
    public void AddTodo_ValidTodo_ReturnsCreatedAtAction()
    {
        // Arrange
        var todo = new Todo { Id = Guid.NewGuid(), Name = "New Todo", IsCompleted = false };

        // Act
        var result = _controller.AddTodo(todo);

        Console.WriteLine(result);
         
        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetTodos), createdResult.ActionName);
        Assert.Equal(201, createdResult.StatusCode);
        _serviceMock.Verify(s => s.AddTodo(todo), Times.Once);
    }

    [Fact]
    public void ToggleTodoCompletion_EmptyGuid_ReturnsBadRequest()
    {
        // Act
        var result = _controller.ToggleTodoCompletion(Guid.Empty);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid todo ID", badRequest.Value);
        _serviceMock.Verify(s => s.ToggleTodoCompletion(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public void ToggleTodoCompletion_ValidGuid_ReturnsNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = _controller.ToggleTodoCompletion(id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _serviceMock.Verify(s => s.ToggleTodoCompletion(id), Times.Once);
    }

    [Fact]
    public void DeleteTodo_EmptyGuid_ReturnsBadRequest()
    {
        // Act
        var result = _controller.DeleteTodo(Guid.Empty);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid todo ID", badRequest.Value);
        _serviceMock.Verify(s => s.DeleteTodo(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public void DeleteTodo_ValidGuid_ReturnsNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = _controller.DeleteTodo(id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _serviceMock.Verify(s => s.DeleteTodo(id), Times.Once);
    }
}
