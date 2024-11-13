using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Service.DTO;
using Service.DTO.Response;

namespace Service.Service;

public class EmployeeService(EmployeeRepository employeeRepository, ProjectRepository projectRepository) {
    private readonly EmployeeRepository _employeeRepository = employeeRepository;
    private readonly ProjectRepository _projectRepository = projectRepository;




    public async Task<ResponseEntity<List<Object>>> SearchEmployeesAsync(string searchTerm = "all", int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            Expression<Func<Employee, bool>> predicate = e =>
            searchTerm == "all" ||
            e.Visa.Contains(searchTerm) ||
            e.FirstName.Contains(searchTerm) ||
            e.LastName.Contains(searchTerm);

            var employees = await _employeeRepository.FindByConditionWithPaginationAsync(predicate, pageNumber, pageSize);

            var employeeBaseResponses = employees.Select(e => new EmployeeBaseResponse
            {
                Id = e.Id,
                Visa = e.Visa,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthDay = e.BirthDay,
                Version = e.Version
            }).ToList();

            return ResponseEntity<List<Object>>.CreateSuccess(employeeBaseResponses.Cast<Object>().ToList());
        }
        catch (Exception ex)
        {

            return ResponseEntity<List<Object>>.Other(ex.Message, 200);
        }
    }





    public async Task<ResponseEntity<EmployeeBaseResponse>> FindById(int id)
    {
        try
        {
            var result = await _employeeRepository.GetByIdAsync(id);
            return ResponseEntity<EmployeeBaseResponse>.CreateSuccess(new EmployeeBaseResponse
            {
                Id = (int)result.Id,
                Visa = result.Visa,
                FirstName = result.FirstName,
                LastName = result.LastName,
                BirthDay = result.BirthDay,
                Version = result.Version
            });
        }
        catch (Exception)
        {
            return ResponseEntity<EmployeeBaseResponse>.Other("Not Found Employee With This ID", 200);
        }
    }
    

    public async Task<ResponseEntity<EmployeeBaseResponse>> CreateEmployeeAsync (EmployeeRequest employeeRequest) {
        if (employeeRequest == null)
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee data cannot be null.");
        }

        if (!IsValidName(employeeRequest.FirstName))
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee First Name is not valid");
        }
        if (!IsValidName(employeeRequest.LastName))
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee Last Name is not valid");
        }
        if (!IsValidVisa(employeeRequest.Visa))
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee Visa is not valid");
        }
        if (!IsValidBirthday(employeeRequest.BirthDay))
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee BirthDay is not valid");
        }
        var employee = new Employee
        {
            Visa = employeeRequest.Visa,
            FirstName = employeeRequest.FirstName,
            LastName = employeeRequest.LastName,
            BirthDay = employeeRequest.BirthDay,
            Version = 1
        };

        await _employeeRepository.AddAsync(employee);

        var employeeResponse = new EmployeeBaseResponse
        {
            Id = (int)employee.Id,
            Visa = employeeRequest.Visa,
            FirstName = employeeRequest.FirstName,
            LastName = employeeRequest.LastName,
            BirthDay= employeeRequest.BirthDay,
            Version = (int)employee.Version,
        };

        return ResponseEntity<EmployeeBaseResponse>.CreateSuccess(employeeResponse);
    }



    public async Task<ResponseEntity<EmployeeBaseResponse>> UpdateGroupAsync(int employeeId, EmployeeRequest employeeRequest)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee == null)
        {
            return ResponseEntity<EmployeeBaseResponse>.NotFound($"Employee with ID {employeeId} not found.");
        }

        if (employeeRequest == null)
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Updated employee data cannot be null.");
        }

        

        if (!IsValidName(employeeRequest.FirstName))
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee First Name is not valid");
        }
        if (!IsValidName(employeeRequest.LastName))
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee Last Name is not valid");
        }
        if (!IsValidVisa(employeeRequest.Visa))
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee Visa is not valid");
        }
        if (!IsValidBirthday(employeeRequest.BirthDay))
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee BirthDay is not valid");
        }

        employee.Visa = employeeRequest.Visa;
        employee.FirstName = employeeRequest.FirstName;
        employee.LastName = employeeRequest.LastName;
        employee.BirthDay = employeeRequest.BirthDay;

        employee.Version++;


        await _employeeRepository.UpdateAsync(employee);

        var employeeResponse = new EmployeeBaseResponse
        {
            Id = employee.Id,
            Visa = employee.Visa,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDay = employee.BirthDay,
            Version = employee.Version,
        };

        return ResponseEntity<EmployeeBaseResponse>.CreateSuccess(employeeResponse);
    }

    public async Task<ResponseEntity<EmployeeBaseResponse>> DeleteEmployeeAsync(int id)
    {
        if (await _projectRepository.IsEmployeeInProjectsAsync(id))
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest($"Employee with ID {id} is in existed projects.");
        }
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
            return ResponseEntity<EmployeeBaseResponse>.NotFound($"Employee with ID {id} not found.");
        }

        await _employeeRepository.DeleteAsync(id);

        var employeeBaseResponse = new EmployeeBaseResponse
        {
            Id = employee.Id,
            Visa = employee.Visa,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDay = employee.BirthDay,
            Version = employee.Version,
        };

        return ResponseEntity<EmployeeBaseResponse>.CreateSuccess(employeeBaseResponse);
    }

    public bool IsValidName(string fullname)
    {
        var regex = new Regex(@"^[A-Z][a-zA-Z0-9#@ ]*$");
        return !string.IsNullOrWhiteSpace(fullname) && regex.IsMatch(fullname);
    }

    public static bool IsValidVisa(string visa)
    {
        const string VisaPattern = @"^[A-Z0-9]{5,10}$"; 

        if (string.IsNullOrWhiteSpace(visa))
        {
            return false; 
        }

        return Regex.IsMatch(visa, VisaPattern);
    }
    public bool IsValidBirthday(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age)) age--;

        return age >= 18;
    }

}