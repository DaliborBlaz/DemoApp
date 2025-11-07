using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.Common.Pagination;
using DemoApp.Application.WorkItems.DTOs;
using MediatR;

namespace DemoApp.Application.WorkItems.Queries;

public class GetWorkItemsQueryHandler 
    : IRequestHandler<GetWorkItemsQuery, PaginatedList<WorkItemDto>>
{
    private readonly IWorkItemRepository _repository;

    public GetWorkItemsQueryHandler(IWorkItemRepository repository) => _repository = repository;

    public async Task<PaginatedList<WorkItemDto>> Handle(GetWorkItemsQuery request, CancellationToken ct)
    {
        var page = await _repository.GetPaginatedAsync(
            request.Search, request.SortBy, request.Descending, request.Page, request.PageSize, ct);

        var dtoItems = page.Items.Select(x => new WorkItemDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            DueDate = x.DueDate,
            AssignedTo = x.AssignedTo
        }).ToList();

        return new PaginatedList<WorkItemDto>(dtoItems, page.TotalCount, page.PageIndex, request.PageSize);
    }
}