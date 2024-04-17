namespace ToDo_Blazor_App_With_LocalStorage.Models;

public record TodoItem(Guid Id, string Title, bool IsDone, bool IsEditing);