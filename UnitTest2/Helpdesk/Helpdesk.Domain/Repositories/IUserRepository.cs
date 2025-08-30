using Helpdesk.Domain.Entities;

namespace Helpdesk.Domain.Repositories;

public interface IUserRepository {
    Task<User> CreateAsync(User user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<bool> DeleteAsync(User user);
}
