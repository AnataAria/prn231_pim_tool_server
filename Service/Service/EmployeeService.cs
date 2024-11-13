using System.Text.RegularExpressions;
using AutoMapper;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Service.DTO;
using Service.DTO.Response;

namespace Service.Service;

public class EmployeeService(EmployeeRepository employeeRepository, ProjectRepository projectRepository, IMapper mapper)
{
    private readonly EmployeeRepository _employeeRepository = employeeRepository;
    private readonly ProjectRepository _projectRepository = projectRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<EmployeeBaseResponse>> SearchEmployeesAsync(string searchTerm = "all", int pageNumber = 1, int pageSize = 10)
    {
        var employees = await _employeeRepository.FindByConditionWithPaginationAsync((e) => searchTerm == "all" ||
            e.Visa.Contains(searchTerm) ||
            e.FirstName.Contains(searchTerm) ||
            e.LastName.Contains(searchTerm), pageNumber, pageSize);

        var employeeBaseResponses = employees.Select(e => mapper.Map<EmployeeBaseResponse>(e)).ToList();

        return employeeBaseResponses;
    }

    public async Task<ResponseEntity<EmployeeBaseResponse>> FindById(int id)
    {
        try
        {
            var result = await _employeeRepository.GetByIdAsync(id);

            return ResponseEntity<EmployeeBaseResponse>.CreateSuccess(new EmployeeBaseResponse
            {
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


    public async Task<ResponseEntity<EmployeeBaseResponse>> CreateEmployeeAsync(EmployeeRequest employeeRequest)
    {
        if (employeeRequest == null)
        {
            return ResponseEntity<EmployeeBaseResponse>.BadRequest("Employee data cannot be null.");
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

        return ResponseEntity<EmployeeBaseResponse>.CreateSuccess(mapper.Map<EmployeeBaseResponse>(employee));
    }



    public async Task<ResponseEntity<EmployeeBaseResponse>> UpdateEmployeeAsync(int employeeId, EmployeeRequest employeeRequest)
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

        return ResponseEntity<EmployeeBaseResponse>.CreateSuccess(mapper.Map<EmployeeBaseResponse>(employee));
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

        return ResponseEntity<EmployeeBaseResponse>.CreateSuccess(mapper.Map<EmployeeBaseResponse>(employee));
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