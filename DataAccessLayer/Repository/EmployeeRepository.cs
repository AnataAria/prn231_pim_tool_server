using System.Linq.Expressions;
using DataAccessLayer.BusinessObject;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repository
{
    public class EmployeeRepository(PIMDatabaseContext context) : BaseRepository<Employee, PIMDatabaseContext>(context)
    {
    }
}
