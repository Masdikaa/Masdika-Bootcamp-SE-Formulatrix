using Helpdesk.Domain.Entities.Enums;
namespace Helpdesk.Domain.Entities;

public class Ticket {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public Priority Priority { get; set; } = Priority.Medium;

    public Guid ReporterId { get; set; }
    public User Reporter { get; set; } = default!;
    public Guid? AssigneeId { get; set; }
    public User? Assignee { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
