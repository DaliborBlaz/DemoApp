using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.Common.Pagination;
using DemoApp.Domain.Entities;
using DemoApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Infrastructure.Repositories;

public class WorkItemRepository : IWorkItemRepository
{
    private readonly DemoAppDbContext _context;

    public WorkItemRepository(DemoAppDbContext context) => _context = context;

    public async Task<Guid> CreateAsync(WorkItem workItem, CancellationToken cancellationToken)
    {
        _context.WorkItems.Add(workItem);
        await _context.SaveChangesAsync(cancellationToken);
        return workItem.Id;
    }

    public async Task<PaginatedList<WorkItem>> GetPaginatedAsync(
        string? search,
        string? sortBy,
        bool descending,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = _context.WorkItems.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(w =>
                w.Title.Contains(search) ||
                (w.Description != null && w.Description.Contains(search)));
        }

        query = sortBy?.ToLower() switch
        {
            "title"   => descending ? query.OrderByDescending(w => w.Title)   : query.OrderBy(w => w.Title),
            "duedate" => descending ? query.OrderByDescending(w => w.DueDate) : query.OrderBy(w => w.DueDate),
            _         => query.OrderByDescending(w => w.CreatedAt)
        };

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<WorkItem>(items, totalCount, page, pageSize);
    }
}