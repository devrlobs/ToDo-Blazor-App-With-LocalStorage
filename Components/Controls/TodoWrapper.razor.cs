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

    protected string CurrentTodoField { get; set; } = string.Empty;
    protected string EditingTodoField { get; set; } = string.Empty;
    protected Guid Id { get; set; } = Guid.NewGuid();

    protected void OnTodoInputKeyPressHandler(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            var id = Guid.NewGuid();
            TodoItem todoItem = new() { Id = id, Title = CurrentTodoField, IsDone = false, IsEditing = false };
            Items.Add(id, todoItem);
            CurrentTodoField = string.Empty;
            OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(todoItem, TodoItemChangeType.Add));
        }
    }

    protected void OnTodoEditingInputHandler(ChangeEventArgs e, TodoItem item)
    {
        TodoItem todoItem = new() { Id = item.Id, Title = e.Value?.ToString() ?? string.Empty, IsDone = item.IsDone, IsEditing = item.IsEditing };
        Items[item.Id] = todoItem;

        //EditingTodoField = e.Value?.ToString() ?? string.Empty;
    }

    protected void OnTodoEditingItemKeyPressHandler(KeyboardEventArgs e, TodoItem item)
    {
        if (e.Key == "Enter")
        {
            // Items[item.Id] = item with {IsEditing = false};
            TodoItem todoItem = new() { Id = item.Id, Title = item.Title, IsDone = item.IsDone, IsEditing = false };
            Items[item.Id] = todoItem;
            OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(Items[item.Id], TodoItemChangeType.Update));
        }
    }

    protected void OnTodoItemDbClick(TodoItem item)
    {

        // Items.Keys.ToList().ForEach(key => Items[key] with {IsEditing = false});
        Items.Keys.ToList().ForEach(key => Items[key].IsEditing = false);
        // foreach(var key in Items.Keys.ToList())
        // {
        //     Items[key].IsEditing = false;
        // }
        //Items[item.Id] = item with {IsEditing = true}
        Items[item.Id].IsEditing = true;

    }

    protected void OnTodoItemInputCheckChanged(ChangeEventArgs e, TodoItem item)
    {
        string isCheckedString = e.Value?.ToString()?.ToLowerInvariant() ?? "false";
        TodoItem todoItem = new() { IsEditing = item.IsEditing };
        todoItem.IsDone = bool.Parse(isCheckedString);
        Items[item.Id] = todoItem;
        OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(Items[item.Id], TodoItemChangeType.Update));

    }

    protected void OnTodoItemRemove(TodoItem item)
    {
        Items.Remove(item.Id);
        OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(Items[item.Id], TodoItemChangeType.Delete));

    }
}












// using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.Components.Web;
// using ToDo_Blazor_App_With_LocalStorage.Models;

// namespace ToDo_Blazor_App_With_LocalStorage.Components.Controls;

// public enum TodoItemChangeType
// {
//     Add,
//     Update,
//     Delete
// }

// public class TodoItemChangedEventArgs(TodoItem item, TodoItemChangeType changeType) : EventArgs
// {
//     public TodoItem Item { get; set; } = item;
//     public TodoItemChangeType ChangeType { get; set; } = changeType;
// }


// public partial class TodoWrapper
// {

//     [Parameter, EditorRequired]
//     public required Dictionary<int, TodoItem> Items { get; set; }

//     [Parameter]
//     public EventCallback<TodoItemChangedEventArgs> OnStateChanged { get; set; }
//     [Parameter]
//     public RenderFragment<TodoItemTemplateData>? ItemTemplate {get;set;}

//     [Parameter]
//     public RenderFragment<TodoInputTemplateData>? InputTemplate {get;set;}



//     [Parameter]
//     public RenderFragment<TodoItem>? ItemTemplate { get; set; }

//     protected string CurrentTodo { get; set; } = string.Empty;
//     protected string CurrentEditingTodo { get; set; } = string.Empty;

//     protected void OnTodoInputSave(string todo)
//     {
//         TodoItem todoItem = new(Items.Count + 1, CurrentTodo, false, false);
//             Items.Add(todoItem.Id, todoItem);
//             OnStateChanged.InvokeAsync(new TodoItemChangedEventArgs(todoItem, TodoItemChangeType.Add));
//     }

//     protected TodoItemTemplateData GetTodoItemTemplateData(TodoItem item)
//     {
//         return new TodoItemTemplateData(item)
//         {
//             OnStateChanged = OnItemChanged,
//             OnRemoved = OnItemRemoved
//         }
//     }


//     protected void OnTodoInputKeyPress(KeyboardEventArgs e)
//     {
//         if (e.Key == "Enter")
//         {
//             TodoItem todoItem = new(Items.Count + 1, CurrentTodo, false, false);
//             Items.Add(todoItem.Id, todoItem);
//             CurrentTodo = string.Empty;
//         }
//     }
// }