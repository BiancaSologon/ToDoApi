namespace ToDoApi.Features.ToDoItems.Models;

public class Comment
{
    public long Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public long ToDoItemId { get; set; }
    public ToDoItem ToDoItem { get; set; } = null!;
}
