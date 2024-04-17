namespace ToDo_Blazor_App_With_LocalStorage.Models;

public record TodoItemTemplateData(TodoItem item)
{
    public TodoItem Item { get; set; } = item;
    public Action<TodoItem>? OnChanged { get; set; }
    public Action<TodoItem>? OnEnterEditMode { get; set; }
    public Action<TodoItem>? OnRemoved { get; set; }
} 