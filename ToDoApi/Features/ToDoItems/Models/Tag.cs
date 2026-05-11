namespace ToDoApi.Features.ToDoItems.Models;

public class Tag
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<ToDoItem> ToDoItems { get; set; } = [];
}
