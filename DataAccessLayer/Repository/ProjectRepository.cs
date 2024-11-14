using DataAccessLayer.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ProjectRepository(PIMDatabaseContext context) : BaseRepository<Project, PIMDatabaseContext>(context)
    {
        public async Task<bool> IsEmployeeInProjectsAsync(long employeeId)
        {
            var list =  await _context.Projects
            .Include(p => p.Employees)
            .FirstOrDefaultAsync(p => p.Employees.Any(e => e.Id == employeeId));
            if (list == null)
                return false;
            return true;
        }
        public async Task<IEnumerable<Project>> FindByConditionWithPaginationAsync(Expression<Func<Project, bool>> predicate, int page = 1, int size = 5)
        {
            return await _dbSet.Include(p => p.GroupProject).ThenInclude(g=> g.Leader).Where(predicate).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<Project> GetByProjectIdAsync(long projectNumber)
        {
            return await _dbSet.FirstOrDefaultAsync(item => item.ProjectNumber == projectNumber) ?? throw new KeyNotFoundException($"Entity with {projectNumber} not found");
        }

        public async Task<bool> IsProjectIdExisted(long projectNumber)
        {
            return await _dbSet.AnyAsync(item => item.ProjectNumber == projectNumber);
        }

    }
}
