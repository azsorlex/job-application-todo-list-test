using TodoListApi.Domain.Models;
using TodoListApi.Infrastructure.Repositories.IRepositories;

namespace TodoListApi.Infrastructure.Repositories;

public class TodosRepository : ITodosRepository
{
    private readonly List<Todo> _todos = [
        new Todo { Id = Guid.NewGuid(), Name = "Buy groceries", IsCompleted = false },
        new Todo { Id = Guid.NewGuid(), Name = "Walk the dog", IsCompleted = true },
        new Todo { Id = Guid.NewGuid(), Name = "Finish homework", IsCompleted = false },
        new Todo { Id = Guid.NewGuid(), Name = "Read a book", IsCompleted = true },
        new Todo { Id = Guid.NewGuid(), Name = "Clean the house", IsCompleted = false }
    ];

    public List<Todo> GetTodos() => _todos;

    public void AddTodo(Todo todo)
    {
        _todos.Add(todo);
    }
}