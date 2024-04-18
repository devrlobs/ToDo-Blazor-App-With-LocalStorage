using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ToDo_Blazor_App_With_LocalStorage.Models;

namespace ToDo_Blazor_App_With_LocalStorage.Components.Controls;

public partial class TodoHeader
{
    protected string CurrentTodoField { get; set; } = string.Empty;

    [Parameter]
    public string Placeholder { get; set; } = "What needs to be done?";

    [Parameter]
    public EventCallback<string> OnSave { get; set; }

    [Parameter, EditorRequired]
    public required TodoItemInputTemplateData HeaderTemplateData { get; set; }

    protected void OnTodoInputKeyPressHandler(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            HeaderTemplateData.OnSave?.Invoke(CurrentTodoField);
            CurrentTodoField = string.Empty;
        }
    }
}