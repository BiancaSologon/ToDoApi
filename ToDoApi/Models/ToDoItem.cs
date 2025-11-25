using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ToDoApi.Models;

public class ToDoItem 
{
    public long Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    [Required]
    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsComplete { get; set; }
   
    public string? Secret { get; set; }
}
