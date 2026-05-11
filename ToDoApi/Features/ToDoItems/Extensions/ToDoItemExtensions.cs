using ToDoApi.Features.ToDoItems.DTOs;
using ToDoApi.Features.ToDoItems.Models;

namespace ToDoApi.Features.ToDoItems.Extensions;

public static class ToDoItemExtensions
{
    public static ToDoItemDTO ToDTO(this ToDoItem toDoItem)
    {
        return new ToDoItemDTO
        {
            Id = toDoItem.Id,
            Name = toDoItem.Name,
            Description = toDoItem.Description,
            IsComplete = toDoItem.IsComplete,
            Secret = toDoItem.Secret,
            Comments = toDoItem
                .Comments?.Select(c => new CommentDTO { Content = c.Content })
                .ToList(),
        };
    }
}
