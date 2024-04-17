namespace ToDo_Blazor_App_With_LocalStorage.Models;

public record TodoItemInputTemplateData
{
    public Action<string>? OnSave {get;set;}
} 