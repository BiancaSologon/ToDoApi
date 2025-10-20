using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly ToDoContext _context;

    public ToDoItemsController(ToDoContext context)
    {
        _context = context;
    }

    // GET: api/ToDoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemsDTO>>> GetToDoItems()
    {
        return await _context.ToDoItems.Select(x => ItemToDTO(x)).ToListAsync();
    }

    // GET: api/ToDoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItemsDTO>> GetToDoItem(long id)
    {
        var toDoItem = await _context.ToDoItems.FindAsync(id);

        if (toDoItem == null)
        {
            return NotFound();
        }

        return ItemToDTO(toDoItem);
    }

    // PUT: api/ToDoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutToDoItem(long id, ToDoItemsDTO toDoItemDTO)
    {
        if (id != toDoItemDTO.Id)
        {
            return BadRequest();
        }

        var toDoItem = await _context.ToDoItems.FindAsync(id);
        if (toDoItem == null)
        {
            return NotFound();
        }

        toDoItem.Name = toDoItemDTO.Name;
        toDoItem.IsComplete = toDoItemDTO.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ToDoItemExists(id))
        {
           return NotFound();
        }

        return NoContent();
    }

    // POST: api/ToDoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItemsDTO toDoItemDTO)
    {
        var toDoItem = new ToDoItem
        {
            IsComplete = toDoItemDTO.IsComplete,
            Name = toDoItemDTO.Name
        };

        _context.ToDoItems.Add(toDoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, ItemToDTO(toDoItem));
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

    private bool ToDoItemExists(long id)
    {
        return _context.ToDoItems.Any(e => e.Id == id);
    }

    private static ToDoItemsDTO ItemToDTO(ToDoItem toDoItem) =>
        new ToDoItemsDTO
        {
            Id = toDoItem.Id,
            Name = toDoItem.Name,
            IsComplete = toDoItem.IsComplete
        };
}
