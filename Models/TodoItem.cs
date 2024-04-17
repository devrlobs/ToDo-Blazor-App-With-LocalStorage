namespace ToDo_Blazor_App_With_LocalStorage.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public bool IsEditing { get; set; }
}