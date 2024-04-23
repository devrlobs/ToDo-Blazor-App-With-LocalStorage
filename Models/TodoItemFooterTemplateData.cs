namespace ToDo_Blazor_App_With_LocalStorage.Models;

public record TodoItemFooterTemplateData
{
    public Action<string>? OnFilterClick { get; set; }
    public int TodoItemCount { get; set; }
    public Action? OnRemoveCompletedClick { get; set; }
    public string FooterSelectedFilter { get; set; } = "All";
}