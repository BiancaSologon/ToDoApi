namespace ToDoApi.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public int ToDoItemId { get; set; }
    public ToDoItem ToDoItem { get; set; } = null!;
}
