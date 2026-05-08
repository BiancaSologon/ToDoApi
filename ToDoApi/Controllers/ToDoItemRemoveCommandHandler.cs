using FluentResults;
using MediatR;
using ToDoApi.Models;

namespace ToDoApi.Controllers;

public class ToDoItemDeleteCommand : IRequest<Result>
{
    public long Id { get; set; }
}


public class ToDoItemRemoveCommandHandler : IRequestHandler<ToDoItemDeleteCommand, Result>
{
    private readonly ToDoContext _context;

    public ToDoItemRemoveCommandHandler(ToDoContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        ToDoItemDeleteCommand request, 
        CancellationToken cancellationToken
    )
    {
        var toDoItem = await _context.ToDoItems.FindAsync(request.Id);
        if (toDoItem == null)
        {
            return Result.Fail("ToDoItem not found");
        }

        _context.ToDoItems.Remove(toDoItem);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }

}
