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
    public async Task<IEnumerable<Group>> GetAllGroupsAsync()
    {
        return await _dbSet.Include(g => g.Leader).Include(g => g.Project).ToListAsync();
    }

    public async Task<Group> CreateGroupAsync(Group group)
    {
        var leaderExists = await _context.Employees.AnyAsync(e => e.Id == group.LeaderId);
        if (!leaderExists)
        {
            throw new InvalidOperationException("The specified leader does not exist.");
        }

        await _dbSet.AddAsync(group);
        await _context.SaveChangesAsync();
        return group;
    }

    public async Task UpdateGroupLeaderAsync(int groupId, long newLeaderId)
    {
        var group = await GetByIdAsync(groupId);
        var leaderExists = await _context.Employees.AnyAsync(e => e.Id == newLeaderId);
        if (!leaderExists)
        {
            throw new InvalidOperationException("The specified leader does not exist.");
        }

        group.LeaderId = newLeaderId;
        await UpdateAsync(group);
    }
    
    public async Task DeleteGroupAsync(long id)
    {
        var group = await _dbSet.Include(g => g.Project)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (group == null)
        {
            throw new KeyNotFoundException($"Group with ID {id} not found.");
        }
        
        if (group.Project != null)
        {
            throw new InvalidOperationException("Cannot delete a group that has an associated project.");
        }
        _dbSet.Remove(group);
        await _context.SaveChangesAsync();
    }

    
}