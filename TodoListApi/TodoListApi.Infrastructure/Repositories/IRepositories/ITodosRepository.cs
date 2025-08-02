using TodoListApi.Domain.Models;

namespace TodoListApi.Infrastructure.Repositories.IRepositories;

public interface ITodosRepository
{
    List<Todo> GetTodos();
}