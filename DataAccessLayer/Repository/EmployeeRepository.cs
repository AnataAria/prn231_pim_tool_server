using DataAccessLayer.BusinessObject;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repository
{
    public class EmployeeRepository(PIMDatabaseContext context) : BaseRepository<Employee, PIMDatabaseContext>(context)
    {

        



    }
}
