using TodoListApi.Domain.Models;
using TodoListApi.Infrastructure.Repositories.IRepositories;

namespace TodoListApi.Infrastructure.Repositories;

public class TodosRepository : ITodosRepository
{
    // Simulating a database with an in-memory list for demonstration purposes.
    private readonly List<Todo> _todos = [];
    
    public List<Todo> GetTodos() => _todos;

    public Todo GetTodo(Guid id) => _todos.FirstOrDefault(t => t.Id == id) ?? throw new KeyNotFoundException("Todo not found");

    public void AddTodo(Todo todo)
    {
        _todos.Add(todo);
    }

    public void SaveChanges()
    {
        // context.SaveChangesAsync(); In a real-world scenario.
    }

    public void DeleteTodo(Guid id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo != null)
        {
            _todos.Remove(todo);
        }
    }
}