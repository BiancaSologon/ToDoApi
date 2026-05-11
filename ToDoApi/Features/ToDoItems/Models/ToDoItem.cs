namespace ToDoApi.Features.ToDoItems.Models;

public class ToDoItem 
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public bool IsComplete { get; set; }
   
    public string? Secret { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DueDate { get; set; }

    public ToDoDetails? ToDoDetails { get; set; }

    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
}
