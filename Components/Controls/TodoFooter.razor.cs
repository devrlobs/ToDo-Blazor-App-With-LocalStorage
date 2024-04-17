using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ToDo_Blazor_App_With_LocalStorage.Models;

namespace ToDo_Blazor_App_With_LocalStorage.Components.Controls;

public partial class TodoFooter
{
    [Parameter]
    public EventCallback<string> OnFilterClick { get; set; }
    
    [Parameter]
    public EventCallback<int> OnRemoveCompletedClick { get; set; }

    [Parameter]
    public int TodoItemCount { get; set; }

    [Parameter, EditorRequired]
    public required TodoItemFooterTemplateData FooterTemplateData { get; set; }

    protected void OnTodoFilterClickedHandler(string filterType)
    {
        FooterTemplateData.OnFilterClick?.Invoke(filterType);
    }

    protected void OnTodoItemCompletedRemove(MouseEventArgs e)
    {
        FooterTemplateData.OnRemoveCompletedClick?.Invoke();
    }
}