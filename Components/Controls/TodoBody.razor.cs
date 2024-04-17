using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ToDo_Blazor_App_With_LocalStorage.Models;

namespace ToDo_Blazor_App_With_LocalStorage.Components.Controls;

public class TodoBodyData
{
    public TodoItem Item { get; set; }
    public EventCallback OnChanged { get; set; }
    public EventCallback OnRemoved { get; set; }
    public EventCallback OnEditing { get; set; }

}

public partial class TodoBody
{
    [Parameter, EditorRequired]
    public required TodoItem Item { get; set; }

    [Parameter]
    public EventCallback OnChanged { get; set; }

    [Parameter]
    public EventCallback OnRemoved { get; set; }

    [Parameter]
    public EventCallback OnEditing { get; set; }

    protected void OnTodoEditingInputHandler(ChangeEventArgs e)
    {
        // TodoItem todoItem = new() { Id = item.Id, Title = e.Value?.ToString() ?? string.Empty, IsDone = item.IsDone, IsEditing = item.IsEditing };
        // Items[item.Id] = todoItem;
        Item = new() { Id = Item.Id, Title = e.Value?.ToString() ?? string.Empty, IsDone = Item.IsDone, IsEditing = Item.IsEditing };
        //Item = Item with {Title=e.Value?.ToString() ?? string.Empty};
    }

    protected void OnTodoEditingItemKeyPressHandler(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            // TodoItem todoItem = new() { Id = item.Id, Title = item.Title, IsDone = item.IsDone, IsEditing = false };
            // Items[item.Id] = todoItem;
            // OnChanged.InvokeAsync(new TodoItemStateChangedEventArgs(Items[item.Id], TodoItemChangeType.Update));
            Item = new() { Id = Item.Id, Title = Item.Title, IsDone = Item.IsDone, IsEditing = false };
            OnChanged.InvokeAsync();
        }
    }

    protected void OnTodoItemDbClick(MouseEventArgs e)
    {
        Item = new() { Id = Item.Id, Title = Item.Title, IsDone = Item.IsDone, IsEditing = true };
        OnEditing.InvokeAsync();
    }

    protected void OnTodoItemInputCheckChanged(ChangeEventArgs e)
    {
        string isCheckedString = e.Value?.ToString()?.ToLowerInvariant() ?? "false";
        Item = new() { Id = Item.Id, Title = Item.Title, IsDone = bool.Parse(isCheckedString), IsEditing = Item.IsEditing };
        OnChanged.InvokeAsync();
    }

    protected void OnTodoItemRemove(MouseEventArgs e)
    {
        OnRemoved.InvokeAsync();
    }
}

