namespace TodoListApi.Domain.Models;

public sealed class Todo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public bool IsCompleted { get; set; } = false;
}