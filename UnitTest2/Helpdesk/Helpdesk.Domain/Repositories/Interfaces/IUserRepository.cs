using Helpdesk.Domain.Entities;

namespace Helpdesk.Domain.Repositories.Interfaces;

public interface IUserRepository {
    Task<User> CreateAsync(User user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> UpdateAsync(User user);
    Task<bool> DeleteAsync(Guid userId);
    // Task<User?> GetByIdAsync(User user);
    // Task<bool> DeleteAsync(User user);
}
