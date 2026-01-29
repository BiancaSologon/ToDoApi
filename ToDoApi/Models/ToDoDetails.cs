namespace ToDoApi.Models;

public class ToDoDetails
{
    public int Id { get; set; }

    public string? Notes { get; set; }
    public string? Location { get; set; }
    public int EstimatedMinutes { get; set; }

    public int ToDoItemId { get; set; }
    public ToDoItem ToDoItem { get; set; } = null!;
}
