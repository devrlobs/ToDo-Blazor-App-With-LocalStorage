namespace ToDo_Blazor_App_With_LocalStorage.Components.Controls;

public partial class TodoBody
{
    
}


// using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.Routing.Template;
// using ToDo_Blazor_App_With_LocalStorage.Models;

// namespace ToDo_Blazor_App_With_LocalStorage.Components.Controls;

// public partial class TodoBody
// {
//     public TodoItem Item
//     {
//         get => TemplateData.Item;
//         set => TemplateData.Item = value;
//     }

//     protected void OnTodoEditingInput(ChangeEventArgs e)
//     {
//         Item = item with { Title = e.Value?.ToString() ?? string.Empty };
//     }

//     protected void OnTodoEditingItemKeyPress(KeyboardEventArgs e, TodoItem item)
//     {
//         if (e.Key == "Enter")
//         {
//             Item = Item with {IsEditing = false };
//             TemplateData.OnChanged?.Invoke(item);
//         }
//     }

//     protected void OnTodoItemDbClick(TodoItem item)
//     {
//          Item = Item with {IsEditing = true };
//             TemplateData.OnEnterEditMode?.Invoke(item);
//     }

//       protected void OnTodoItemInputCheckChanged(ChangeEventArgs e)
//     {
//         string isCheckedString = e.Value?.ToString()?.ToLowerInvariant() ?? "false";
//          Item = Item with {IsDone = bool.Parse(isCheckedString) };
//             TemplateData.OnChanged?.Invoke(item);
//     }

//     protected void OnTodoItemRemove(TodoItem item)
//     {
//         TemplateData.OnRemoved?.Invoke(item);
//     }

// }