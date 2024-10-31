using DataAccessLayer.BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class UserRepository(PIMDatabaseContext context) : BaseRepository<User, PIMDatabaseContext>(context)
{
    public async Task<User> SearchUserByUsername (string? username) {
        var user = await _dbSet.FirstOrDefaultAsync(u => u.Username == username) ?? throw new KeyNotFoundException($"Not Found User with username {username}");
        return user;
    }
}