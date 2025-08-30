using Helpdesk.Domain.Entities;
using Helpdesk.Domain.Repositories;
using Helpdesk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository {

    private readonly HelpdeskDbContext _db;

    public TicketRepository(HelpdeskDbContext db) => _db = db;

    public async Task<Ticket> CreateAsync(Ticket ticket) {
        await _db.AddAsync(ticket);
        await _db.SaveChangesAsync();
        return ticket;
    }

    public Task<List<Ticket>> GetAllAsync() {
        var tickets = _db.Tickets.ToListAsync();
        return tickets;
    }

    public async Task<Ticket?> GetByIdAsync(Ticket ticket) {
        var existingTicket = await _db.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);
        if (existingTicket == null) {
            return null;
        }
        return existingTicket;
    }

    public async Task<Ticket?> UpdateAsync(Ticket ticket) {
        var existingTicket = await _db.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);
        if (existingTicket == null) {
            return null;
        }

        existingTicket.Title = ticket.Title;
        existingTicket.Description = ticket.Description;
        existingTicket.Status = ticket.Status;
        existingTicket.Priority = ticket.Priority;
        existingTicket.ReporterId = ticket.ReporterId;
        existingTicket.Reporter = ticket.Reporter;
        existingTicket.AssigneeId = ticket.AssigneeId;
        existingTicket.Assignee = ticket.Assignee;
        existingTicket.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return existingTicket;
    }

    public async Task<bool> DeleteAsync(Ticket ticket) {
        var ticketToDelete = await _db.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);

        if (ticketToDelete == null) {
            return false;
        }

        _db.Tickets.Remove(ticketToDelete);
        await _db.SaveChangesAsync();
        return true;
    }

}
