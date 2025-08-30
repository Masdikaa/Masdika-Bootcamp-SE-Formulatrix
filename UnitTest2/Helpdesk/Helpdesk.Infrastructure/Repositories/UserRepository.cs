using Helpdesk.Domain.Entities;
using Helpdesk.Domain.Repositories;
using Helpdesk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Infrastructure.Repositories;

public class UserRepository : IUserRepository {

    private readonly HelpdeskDbContext _db;

    public UserRepository(HelpdeskDbContext db) => _db = db;

    public async Task<User> CreateAsync(User user) {
        await _db.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetAllAsync() {
        var users = await _db.Users.ToListAsync();
        return users;
    }

    public async Task<User?> GetByIdAsync(User user) {
        var userToGet = await _db.Users.FirstOrDefaultAsync(user => user.Id == user.Id);
        if (userToGet == null) {
            return null;
        }
        return userToGet;
    }

    public async Task<User?> UpdateAsync(User user) {
        var existingUser = await _db.Users.FirstOrDefaultAsync(user => user.Id == user.Id);
        if (existingUser == null) {
            return null;
        }

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;

        await _db.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> DeleteAsync(User user) {
        var userToDelete = await _db.Users.FirstOrDefaultAsync(user => user.Id == user.Id);

        if (userToDelete == null) {
            return false;
        }

        _db.Users.Remove(userToDelete);
        await _db.SaveChangesAsync();
        return true;
    }


}