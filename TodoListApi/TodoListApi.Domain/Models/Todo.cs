namespace TodoListApi.Domain.Models;

public sealed class Todo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}