using FluentResults;
using MediatR;
using ToDoApi.Features.Common;

namespace ToDoApi.Features.ToDoItems.Commands;

public class ToDoItemDeleteCommand : IRequest<Result>
{
    public long Id { get; set; }
}

public class ToDoItemDeleteCommandHandler : IRequestHandler<ToDoItemDeleteCommand, Result>
{
    private readonly ToDoContext _context;

    public ToDoItemDeleteCommandHandler(ToDoContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        ToDoItemDeleteCommand request,
        CancellationToken cancellationToken
    )
    {
        var toDoItem = await _context.ToDoItems.FindAsync([request.Id], cancellationToken);
        if (toDoItem == null)
        {
            return Result.Fail("ToDoItem not found");
        }

        _context.ToDoItems.Remove(toDoItem);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
