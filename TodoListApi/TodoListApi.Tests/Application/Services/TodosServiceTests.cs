using Moq;
using TodoListApi.Application.Services;
using TodoListApi.Domain.Models;
using TodoListApi.Infrastructure.Repositories.IRepositories;

namespace TodoListApi.Tests.Application.Services;

public class TodosServiceTests
{
    private readonly Mock<ITodosRepository> _repositoryMock;
    private readonly TodosService _service;

    public TodosServiceTests()
    {
        _repositoryMock = new Mock<ITodosRepository>();
        _service = new TodosService(_repositoryMock.Object);
    }

    [Fact]
    public void GetTodos_ReturnsListOfTodos()
    {
        // Arrange
        var todos = new List<Todo>
            {
                new Todo { Id = Guid.NewGuid(), Name = "Test 1", IsCompleted = false },
                new Todo { Id = Guid.NewGuid(), Name = "Test 2", IsCompleted = true }
            };
        _repositoryMock.Setup(r => r.GetTodos()).Returns(todos);

        // Act
        var result = _service.GetTodos();

        // Assert
        Assert.Equal(todos, result);
        _repositoryMock.Verify(r => r.GetTodos(), Times.Once);
    }

    [Fact]
    public void AddTodo_CallsRepositoryAddTodo()
    {
        // Arrange
        var todo = new Todo { Id = Guid.NewGuid(), Name = "New Todo", IsCompleted = false };

        // Act
        _service.AddTodo(todo);

        // Assert
        _repositoryMock.Verify(r => r.AddTodo(todo), Times.Once);
    }

    [Fact]
    public void ToggleTodoCompletion_TogglesIsCompletedAndUpdatesTodo()
    {
        // Arrange
        var id = Guid.NewGuid();
        var todo = new Todo { Id = id, Name = "Toggle Todo", IsCompleted = false };
        _repositoryMock.Setup(r => r.GetTodo(id)).Returns(todo);

        // Act
        _service.ToggleTodoCompletion(id);

        // Assert
        Assert.True(todo.IsCompleted);
        _repositoryMock.Verify(r => r.GetTodo(id), Times.Once);
        _repositoryMock.Verify(r => r.UpdateTodo(todo), Times.Once);
    }

    [Fact]
    public void DeleteTodo_CallsRepositoryDeleteTodo()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        _service.DeleteTodo(id);

        // Assert
        _repositoryMock.Verify(r => r.DeleteTodo(id), Times.Once);
    }
}
