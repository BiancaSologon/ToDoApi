using FluentResults;
using MediatR;
using ToDoApi.Features.Common;

namespace ToDoApi.Features.ToDoItems.Commands;

public class ToDoItemUpdateCommand : IRequest<Result>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Description { get; set; }
    public string? Secret { get; set; }
}

public class ToDoItemUpdateCommandHandler : IRequestHandler<ToDoItemUpdateCommand, Result>
{
    private readonly ToDoContext _context;

    public ToDoItemUpdateCommandHandler(ToDoContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        ToDoItemUpdateCommand request,
        CancellationToken cancellationToken
    )
    {
        var toDoItem = await _context.ToDoItems.FindAsync(request.Id);
        if (toDoItem == null)
        {
            return Result.Fail("ToDo item not found");
        }

        toDoItem.Name = request.Name;
        toDoItem.IsComplete = request.IsComplete;
        toDoItem.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
