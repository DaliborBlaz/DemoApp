namespace DemoApp.Application.WorkItems.DTOs;

public class WorkItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string? AssignedTo { get; set; }   // ✅ This is missing in your error
}