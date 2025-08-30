
using Helpdesk.Domain.Entities;

namespace Helpdesk.Domain.Repositories;

public interface ITicketRepository {
    Task<Ticket> CreateAsync(Ticket ticket);
    Task<List<Ticket>> GetAllAsync();
    Task<Ticket?> GetByIdAsync(Ticket ticket);
    Task<Ticket?> UpdateAsync(Ticket ticket);
    Task<bool> DeleteAsync(Ticket ticket);
}