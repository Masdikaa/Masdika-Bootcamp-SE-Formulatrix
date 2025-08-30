using Helpdesk.Domain.Entities;

namespace Helpdesk.Domain.Repositories.Interfaces;

public interface ITicketRepository {
    Task<Ticket> CreateAsync(Ticket ticket);
    Task<List<Ticket>> GetAllAsync();
    Task<Ticket?> GetByIdAsync(Guid ticketId);
    Task<Ticket?> UpdateAsync(Ticket ticket);
    Task<bool> DeleteAsync(Guid ticketId);
    // Task<Ticket?> GetByIdAsync(Ticket ticket);
    // Task<bool> DeleteAsync(Ticket ticket);
}