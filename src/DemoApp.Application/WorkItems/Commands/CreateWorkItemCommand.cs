
using MediatR;

namespace DemoApp.Application.WorkItems.Commands;

public class CreateWorkItemCommand: IRequest<Guid>
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string? AssignedTo { get; set; }
}