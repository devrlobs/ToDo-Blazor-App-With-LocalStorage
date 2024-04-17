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

    protected void OnTodoInputSaveHandler(string todo)
    {
        TodoItem todoItem = new(Guid.NewGuid(), todo, false, false);
        Items.Add(todoItem.Id, todoItem);
        OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(todoItem, TodoItemChangeType.Add));
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
            OnRemoved = OnTodoItemRemove
        };
    }
    protected void OnTodoItemRemove(TodoItem item)
    {
        Items.Remove(item.Id);
        OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(item, TodoItemChangeType.Delete));
    }

    protected void OnFilterClickHandler(string filterType)
    {
        OnFilterChanged.InvokeAsync(filterType);
    }
}