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
        await jSRuntime.InvokeVoidAsync("setItem", "todoItems", JsonSerializer.Serialize(TodoItem));
    }

    public async Task<Dictionary<Guid, TodoItem>> GetTodoItemsAsync()
    {
        string? todoItems = await jSRuntime.InvokeAsync<string?>("getItem", "todoItems");
        if (string.IsNullOrWhiteSpace(todoItems)) return [];
        return JsonSerializer.Deserialize<Dictionary<Guid, TodoItem>>(todoItems) ?? [];
    }

    public async Task<Dictionary<Guid, TodoItem>> GetFilteredTodoItemAsync(string filter)
    {
        Dictionary<Guid, TodoItem> tempTodo = await GetTodoItemsAsync();
        switch (filter)
        {
            case "All":
                tempTodo = tempTodo.ToDictionary(p => p.Key, p => p.Value);
                break;
            case "Active":
                tempTodo = tempTodo.Where(o => o.Value.IsDone == false).ToDictionary(p => p.Key, p => p.Value);
                break;
            case "Completed":
                tempTodo = tempTodo.Where(o => o.Value.IsDone == true).ToDictionary(p => p.Key, p => p.Value);
                break;
        }
        return tempTodo;
    }

}