using System.Linq.Expressions;
using DataAccessLayer.BusinessObject;
using System.Linq;


namespace DataAccessLayer.Repository
{
    public class EmployeeRepository(PIMDatabaseContext context) : BaseRepository<Employee, PIMDatabaseContext>(context)
    {
        public Employee GetById(long id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }
    }
}
