namespace ToDoApi.Features.ToDoItems.Models;

public class ToDoDetails
{
    public long Id { get; set; }

    public string? Notes { get; set; }
    public string? Location { get; set; }
    public int EstimatedMinutes { get; set; }

    public long ToDoItemId { get; set; }
    public ToDoItem ToDoItem { get; set; } = null!;
}
