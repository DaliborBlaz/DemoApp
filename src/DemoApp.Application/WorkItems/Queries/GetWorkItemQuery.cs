using DemoApp.Application.Common.Pagination;
using DemoApp.Application.WorkItems.DTOs;
using MediatR;

namespace DemoApp.Application.WorkItems.Queries;

public class GetWorkItemsQuery : IRequest<PaginatedList<WorkItemDto>>
{
    public string? Search { get; set; }
    public string? SortBy { get; set; }
    public bool Descending { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}