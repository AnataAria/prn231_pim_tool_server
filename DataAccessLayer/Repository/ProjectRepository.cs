using DataAccessLayer.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    }
}
