using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using ToDo_Blazor_App_With_LocalStorage.Models;

namespace ToDo_Blazor_App_With_LocalStorage.Components.Controls;

public enum TodoItemChangeType
{
    Add,
    Update,
    Delete
}

public class TodoItemStateChangedEventArgs(TodoItem item, TodoItemChangeType changeType) : EventArgs
{
    public TodoItem Item { get; set; } = item;
    public TodoItemChangeType ChangeType { get; set; } = changeType;
}

public partial class TodoWrapper
{
    [Parameter, EditorRequired]
    public required Dictionary<Guid, TodoItem> Items { get; set; }

    [Parameter]
    public EventCallback<TodoItemStateChangedEventArgs> OnChanged { get; set; }

    [Parameter]
    public EventCallback<string> OnFilterChanged { get; set; }

    [Parameter]
    public RenderFragment<TodoItemTemplateData>? ItemTemplate { get; set; }

    [Parameter]
    public RenderFragment<TodoItemInputTemplateData>? InputTemplate { get; set; }

    [Parameter]
    public RenderFragment<TodoItemFooterTemplateData>? FooterTemplate { get; set; }

    public string Filter { get; set; } = "All";

    protected void OnTodoInputSaveHandler(string todo)
    {
        TodoItem todoItem = new(Guid.NewGuid(), todo, false, false);
        Items.Add(todoItem.Id, todoItem);
        OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(todoItem, TodoItemChangeType.Add));
    }
    protected void OnToggleStatusButtonHandler()
    {
        var tempItems = Items.Where(o => o.Value.IsDone == false).ToDictionary(p => p.Key, p => p.Value);

        if (tempItems.Count > 0)
        {
            foreach (var todo in tempItems)
            {
                Items[todo.Value.Id] = todo.Value with { IsDone = true };
                OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(todo.Value, TodoItemChangeType.Update));
            }
        }
        else
        {
             var itemsDone = Items.Where(o => o.Value.IsDone == true).ToDictionary(p => p.Key, p => p.Value);
              foreach (var todo in itemsDone)
            {
                Items[todo.Value.Id] = todo.Value with { IsDone = false };
                OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(todo.Value, TodoItemChangeType.Update));
            }
        }

    }

    protected void OnItemChanged(TodoItem item)
    {
        Items[item.Id] = item;
        OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(item, TodoItemChangeType.Update));
    }

    protected TodoItemTemplateData GetTodoItemTemplateData(TodoItem item)
    {
        return new TodoItemTemplateData(item)
        {
            OnChanged = OnItemChanged,
            OnRemoved = OnTodoItemRemove,
            OnRemovedCompleted = OnTodoItemRemoveCompleted
        };
    }
    protected void OnTodoItemRemove(TodoItem item)
    {
        Items.Remove(item.Id);
        OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(item, TodoItemChangeType.Delete));
    }

    protected void OnTodoItemRemoveCompleted()
    {
        var tempItems = Items.Where(o => o.Value.IsDone == true).ToDictionary(p => p.Key, p => p.Value);

        foreach (var todo in tempItems)
        {
            Items.Remove(todo.Value.Id);
            OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(todo.Value, TodoItemChangeType.Delete));
        }
    }

    protected void OnFilterClickHandler(string filterType)
    {
        Filter = filterType;
        OnFilterChanged.InvokeAsync(filterType);
    }
}