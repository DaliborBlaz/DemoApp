using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.WorkItems.Commands;
using DemoApp.Domain.Entities;
using MediatR;

namespace DemoApp.Application.WorkItems.Commands;

public class CreateWorkItemCommandHandler(IWorkItemRepository repository) : IRequestHandler<CreateWorkItemCommand, Guid>
{
    private readonly IWorkItemRepository _repository = repository;
    
    public async Task<Guid> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var workItem = new WorkItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            AssignedTo = request.AssignedTo,
            CreatedAt = DateTime.UtcNow
        };
        return await _repository.CreateAsync(workItem, cancellationToken);
    }
}