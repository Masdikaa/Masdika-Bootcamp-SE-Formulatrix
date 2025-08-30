namespace Helpdesk.Domain.Entities;

public class User {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; } = true;

    public List<Ticket> ReportedTickets { get; set; } = new List<Ticket>();
    public List<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
}