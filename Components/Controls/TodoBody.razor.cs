using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Features;
using ToDo_Blazor_App_With_LocalStorage.Models;

namespace ToDo_Blazor_App_With_LocalStorage.Components.Controls;

public partial class TodoBody
{
    [Parameter, EditorRequired]
    public required TodoItemTemplateData TemplateData { get; set; }
    public TodoItem Item
    {
        get => TemplateData.Item;
        set => TemplateData.Item = value;
    }
    protected void OnTodoEditingInputHandler(ChangeEventArgs e)
    {
        Item = Item with { Title = e.Value?.ToString() ?? string.Empty };
    }

    protected void OnTodoEditingItemKeyPressHandler(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            Item = Item with { IsEditing = false };
            TemplateData.OnChanged?.Invoke(Item);
        }
    }

    protected void OnTodoItemDbClick(MouseEventArgs e)
    {
        Item = Item with { IsEditing = true };
        TemplateData.OnEnterEditMode?.Invoke(Item);
    }

    protected void OnTodoItemInputCheckChanged(ChangeEventArgs e)
    {
        string isCheckedString = e.Value?.ToString()?.ToLowerInvariant() ?? "false";
        Item = Item with { IsDone = bool.Parse(isCheckedString) };
        TemplateData.OnChanged?.Invoke(Item);
    }

    protected void OnTodoItemRemove(MouseEventArgs e)
    {
        TemplateData.OnRemoved?.Invoke(Item);
    }
}

