using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Features.Common;
using ToDoApi.Features.ToDoItems.DTOs;
using ToDoApi.Features.ToDoItems.Models;

namespace ToDoApi.Features.ToDoItems.Queries;

public class ToDoItemQueryHandler : IRequestHandler<ToDoItemQuery, ToDoItemDTO>
{
    private readonly ToDoContext _context;

    public ToDoItemQueryHandler(ToDoContext context)
    {
        _context = context;
    }

    public async Task<ToDoItemDTO?> Handle(
        ToDoItemQuery request,
        CancellationToken cancellationToken
    )
    {
        var toDoItem = await _context
            .ToDoItems
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        return ToDoItemsDTO(toDoItem);
    }

    private static ToDoItemDTO? ToDoItemsDTO(ToDoItem? toDoItem)
    {
        if (toDoItem is null)
        {
            return null;
        }

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
