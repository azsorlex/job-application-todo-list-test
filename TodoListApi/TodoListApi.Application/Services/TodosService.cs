using TodoListApi.Application.Services.IServices;
using TodoListApi.Domain.Models;
using TodoListApi.Infrastructure.Repositories.IRepositories;

namespace TodoListApi.Application.Services;

public class TodosService(ITodosRepository repository) : ITodosService
{
    public List<Todo> GetTodos() => repository.GetTodos();
}