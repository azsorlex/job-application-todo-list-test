using TodoListApi.Domain.Models;

namespace TodoListApi.Application.Services.IServices;

public interface ITodosService
{
    List<Todo> GetTodos();
}