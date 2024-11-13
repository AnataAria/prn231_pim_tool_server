using System.ComponentModel.DataAnnotations;
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

public class EmployeeRequest
{
    [Required(ErrorMessage = "Visa is required")]
    [RegularExpression(@"^[A-Z0-9]{5,10}$", ErrorMessage = "Employee Visa is not valid")]
    public string? Visa {get; set;}
    [Required(ErrorMessage = "First Name is required")]
    [RegularExpression(@"^[A-Z][a-zA-Z0-9#@ ]*$", ErrorMessage = "Employee First Name is not valid")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Last Name is required")]
    [RegularExpression(@"^[A-Z][a-zA-Z0-9#@ ]*$", ErrorMessage = "Employee Last Name is not valid")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Birthday is required")]
    public DateTime BirthDay { get; set; }
}