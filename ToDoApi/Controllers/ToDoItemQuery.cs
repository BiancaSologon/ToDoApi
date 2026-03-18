using MediatR;
using ToDoApi.Models;

namespace ToDoApi.Controllers;

public class ToDoItemQuery : IRequest<ToDoItemDTO?>
{
    public long Id { get; set; }
}


//request ->handler(request, response) -> response
//queries / commands
