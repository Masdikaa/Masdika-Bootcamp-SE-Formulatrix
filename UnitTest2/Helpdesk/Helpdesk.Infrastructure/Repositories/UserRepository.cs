using Helpdesk.Domain.Entities;
using Helpdesk.Domain.Repositories.Interfaces;
using Helpdesk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Infrastructure.Repositories;

public class UserRepository : IUserRepository {

    private readonly HelpdeskDbContext _db;

    public UserRepository(HelpdeskDbContext db) => _db = db;

    public async Task<User> CreateAsync(User user) {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetAllAsync() {
        var users = await _db.Users.ToListAsync();
        return users;
    }

    public async Task<User?> GetByIdAsync(Guid userId) {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) {
            return null;
        }

        return user;
    }

    public async Task<User?> UpdateAsync(User user) {
        var userToUpdate = await _db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

        if (userToUpdate == null) {
            return null;
        }

        userToUpdate.Name = user.Name;
        userToUpdate.Email = user.Email;

        await _db.SaveChangesAsync();
        return userToUpdate;
    }

    public async Task<bool> DeleteAsync(Guid userId) {
        var userToDelete = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (userToDelete == null) {
            return false;
        }

        _db.Users.Remove(userToDelete);
        await _db.SaveChangesAsync();
        return true;
    }


}
