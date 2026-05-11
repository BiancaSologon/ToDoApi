using MediatR;
using ToDoApi.Features.ToDoItems.DTOs;

namespace ToDoApi.Features.ToDoItems.Queries;

public class ToDoItemQuery : IRequest<ToDoItemDTO?>
{
    public long Id { get; set; }
}