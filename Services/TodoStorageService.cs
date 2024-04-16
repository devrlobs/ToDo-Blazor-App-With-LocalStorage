using System.Text.Json;
using Microsoft.JSInterop;
using ToDo_Blazor_App_With_LocalStorage.Models;

namespace ToDo_Blazor_App_With_LocalStorage.Services;

public class TodoStorageService(IJSRuntime jSRuntime)
{
    private Guid Id { get; set; } = Guid.NewGuid();
    private readonly IJSRuntime jSRuntime = jSRuntime;

    public async Task SaveTodoItemAsync(Dictionary<Guid, TodoItem> TodoItem)
    {
        await jSRuntime.InvokeVoidAsync("localStorage.setItem", "todoItems", JsonSerializer.Serialize(TodoItem));
    }

    public async Task<Dictionary<Guid, TodoItem>> GetTodoItemsAsync()
    {
        string? todoItems = await jSRuntime.InvokeAsync<string?>("localStorage.getItem", "todoItems");
        if (string.IsNullOrWhiteSpace(todoItems)) return [];
        return JsonSerializer.Deserialize<Dictionary<Guid, TodoItem>>(todoItems) ?? [];
    }

}