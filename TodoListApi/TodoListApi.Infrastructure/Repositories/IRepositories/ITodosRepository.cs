using TodoListApi.Domain.Models;

namespace TodoListApi.Infrastructure.Repositories.IRepositories;

public interface ITodosRepository
{
    List<Todo> GetTodos();

    Todo GetTodo(Guid id);

    void AddTodo(Todo todo);

    void UpdateTodo(Todo todo);

    void DeleteTodo(Guid id);
}