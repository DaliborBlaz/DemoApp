using DemoApp.Application.Common.Pagination;
using DemoApp.Domain.Entities;

namespace DemoApp.Application.Common.Interfaces;

public interface IWorkItemRepository
{
    Task<Guid> CreateAsync(WorkItem workItem, CancellationToken cancellationToken);

    Task<PaginatedList<WorkItem>> GetPaginatedAsync(
        string? search,
        string? sortBy,
        bool descending,
        int page,
        int pageSize,
        CancellationToken cancellationToken);
}