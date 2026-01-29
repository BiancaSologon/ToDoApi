namespace ToDoApi.Models;

public class ToDoItem 
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public bool IsComplete { get; set; }
   
    public string? Secret { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DueDate { get; set; }

    public ToDoDetails? ToDoDetails { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
