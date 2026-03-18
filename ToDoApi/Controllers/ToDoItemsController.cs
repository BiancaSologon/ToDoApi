using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

namespace ToDoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly ToDoContext _context;
    private readonly IMediator _mediator;

    public ToDoItemsController(ToDoContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    //GET comments for 1 todoitem: /api/ToDoItems/{id}/comments/{commentId}

    // api/todoitems/5/tags/2

    // GET: api/ToDoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> GetToDoItems(
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(new ToDoItemsQuery(), cancellationToken);

        return Ok(result);
    }

    // GET: api/ToDoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItemDTO>> GetToDoItem(
        long id,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(new ToDoItemQuery { Id = id }, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // PUT: api/ToDoItems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutToDoItem(
        long id,
        ToDoItemDTO toDoItemDTO,
        CancellationToken cancellationToken
    )
    {
        if (id != toDoItemDTO.Id)
        {
            return BadRequest();
        }

        var updateCommand = new ToDoItemUpdateCommand
        {
            Id = id,
            Name = toDoItemDTO.Name,
            IsComplete = toDoItemDTO.IsComplete,
            Description = toDoItemDTO.Description,
        };

        var result = await _mediator.Send(updateCommand, cancellationToken);

        if (result.IsFailed)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/ToDoItems
    [HttpPost]
    public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItemDTO toDoItemDTO)
    {
        var createCommand = new ToDoItemCreateCommand
        {
            Name = toDoItemDTO.Name,
            IsComplete = toDoItemDTO.IsComplete,
            Description = toDoItemDTO.Description,
            Secret = toDoItemDTO.Secret,
            Comments = toDoItemDTO.Comments,
        };
        var createdToDoItemId = await _mediator.Send(createCommand);

        return CreatedAtAction(nameof(GetToDoItem), new { id = createdToDoItemId });
    }

    // DELETE: api/ToDoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoItem(long id)
    {
        var toDoItem = await _context.ToDoItems.FindAsync(id);
        if (toDoItem == null)
        {
            return NotFound();
        }

        _context.ToDoItems.Remove(toDoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
