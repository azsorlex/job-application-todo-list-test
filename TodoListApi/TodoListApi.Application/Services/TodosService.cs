using TodoListApi.Application.Services.IServices;
using TodoListApi.Domain.Models;
using TodoListApi.Infrastructure.Repositories.IRepositories;

namespace TodoListApi.Application.Services;

public class TodosService(ITodosRepository repository) : ITodosService
{
    public List<Todo> GetTodos() => repository.GetTodos();

    public void AddTodo(Todo todo)
    {
        repository.AddTodo(todo);
    }

    public void DeleteTodo(Guid id)
    {
        repository.DeleteTodo(id);
    }
}