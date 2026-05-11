using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Features.Common;
using ToDoApi.Features.ToDoItems.DTOs;
using ToDoApi.Features.ToDoItems.Extensions;

namespace ToDoApi.Features.ToDoItems.Queries;

public class PageList<T>
{
    public List<T> List { get; set; } = [];
    public int TotalCount { get; set; }
}

public class ToDoItemsQuery : IRequest<PageList<ToDoItemDTO>>
{
    public int PageNumber { get; set; }
    public int Limit { get; set; }
}

public class ToDoItemsQueryHandler : IRequestHandler<ToDoItemsQuery, PageList<ToDoItemDTO>>
{
    private readonly ToDoContext _context;

    public ToDoItemsQueryHandler(ToDoContext context)
    {
        _context = context;
    }

    public async Task<PageList<ToDoItemDTO>> Handle(
        ToDoItemsQuery request,
        CancellationToken cancellationToken
    )
    {
        var count = await _context.ToDoItems.CountAsync(cancellationToken);

        if (count == 0)
        {
            return new PageList<ToDoItemDTO>();
        }

        var list = await _context
            .ToDoItems.OrderBy(x => x.Id)
            .Skip(request.PageNumber * request.Limit)
            .Take(request.Limit)
            .Select(x => x.ToDTO())
            .ToListAsync(cancellationToken);

        return new PageList<ToDoItemDTO> { List = list, TotalCount = count };
    }
}
