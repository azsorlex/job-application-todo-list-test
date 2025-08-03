using TodoListApi.Domain.Models;

namespace TodoListApi.Infrastructure.Repositories.IRepositories;

public interface ITodosRepository
{
    List<Todo> GetTodos();

    void AddTodo(Todo todo);

    void DeleteTodo(Guid id);
}