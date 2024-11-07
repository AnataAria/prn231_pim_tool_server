using DataAccessLayer.BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class GroupRepository(PIMDatabaseContext context) : BaseRepository<Group, PIMDatabaseContext>(context)
{
    public async Task<IEnumerable<Group>> GetByLeaderIdAsync(long leaderId)
    {
        return await _dbSet.Where(g => g.LeaderId == leaderId).ToListAsync();
    }
    public async Task<Group> GetGroupWithProjectAsync(long groupId)
    {
        return await _dbSet.Include(g => g.Project)
                            .FirstOrDefaultAsync(g => g.Id == groupId)
               ?? throw new KeyNotFoundException($"Group with ID {groupId} not found.");
    }
    public async Task UpdateGroupLeaderAsync(int groupId, long newLeaderId)
    {
        var group = await GetByIdAsync(groupId);
        group.LeaderId = newLeaderId;
        await UpdateAsync(group);
    }
}