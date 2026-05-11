using MediatR;
using ToDoApi.Features.Common;
using ToDoApi.Features.ToDoItems.DTOs;
using ToDoApi.Features.ToDoItems.Models;

namespace ToDoApi.Features.ToDoItems.Commands;

public class ToDoItemCreateCommand : IRequest<long>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Description { get; set; }
    public string? Secret { get; set; }
    public List<CommentDTO> Comments { get; set; } = [];
}

public class ToDoItemCreateCommandHandler : IRequestHandler<ToDoItemCreateCommand, long>
{
    private readonly ToDoContext _context;

    public ToDoItemCreateCommandHandler(ToDoContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(
        ToDoItemCreateCommand request,
        CancellationToken cancellationToken
    )
    {
        var toDoItem = new ToDoItem
        {
            Name = request.Name,
            IsComplete = request.IsComplete,
            Description = request.Description,
            Secret = request.Secret,
            Comments =
            [
                .. request.Comments!.Select(c => new Comment
                {
                    Content = c.Content,
                    CreatedAt = DateTime.UtcNow,
                }),
            ],
        };

        _context.ToDoItems.Add(toDoItem);
        await _context.SaveChangesAsync(cancellationToken);

        return toDoItem.Id;
    }
}
