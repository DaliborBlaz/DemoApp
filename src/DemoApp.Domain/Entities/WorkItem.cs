using System;

namespace DemoApp.Domain.Entities
{
    public class WorkItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty;
        public WorkItemStatus Status { get; set; } = WorkItemStatus.Todo;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public string? AssignedTo { get; set; } //Later turn into FK

    }

    public enum WorkItemStatus
    {
        Todo,
        InProgress,
        Done
    }
}