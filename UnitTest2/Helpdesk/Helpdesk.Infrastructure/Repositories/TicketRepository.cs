using Helpdesk.Domain.Entities;
using Helpdesk.Domain.Repositories.Interfaces;
using Helpdesk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository {

    private readonly HelpdeskDbContext _db;

    public TicketRepository(HelpdeskDbContext db) => _db = db;

    public async Task<Ticket> CreateAsync(Ticket ticket) {
        await _db.Tickets.AddAsync(ticket);
        await _db.SaveChangesAsync();
        return ticket;
    }

    public async Task<List<Ticket>> GetAllAsync() {
        var tickets = await _db.Tickets.ToListAsync();
        return tickets;
    }

    public async Task<Ticket?> GetByIdAsync(Guid ticketId) {
        var ticket = await _db.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null) {
            return null;
        }

        return ticket;
    }

    public async Task<Ticket?> UpdateAsync(Ticket ticket) {
        var existingTicket = await _db.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);
        if (existingTicket == null) {
            return null;
        }

        // Only update properties that should be changed.
        existingTicket.Title = ticket.Title;
        existingTicket.Description = ticket.Description;
        existingTicket.Status = ticket.Status;
        existingTicket.Priority = ticket.Priority;
        existingTicket.AssigneeId = ticket.AssigneeId; // Update the foreign key, not the navigation property
        existingTicket.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return existingTicket;
    }

    public async Task<bool> DeleteAsync(Guid ticketId) {
        var ticketToDelete = await _db.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticketToDelete == null) {
            return false;
        }

        _db.Tickets.Remove(ticketToDelete);
        await _db.SaveChangesAsync();
        return true;
    }

}