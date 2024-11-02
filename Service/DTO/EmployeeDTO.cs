using DataAccessLayer.BusinessObject;

namespace Service.DTO;

public class EmployeeBaseResponse
{
    public long Id {get; set;}
    public string? Visa {get; set;}
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDay { get; set; }
    public long Version { get; set; }

}
