using System.Diagnostics;
using DataAccessLayer;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Service.DTO;
using Service.DTO.Response;

namespace Service.Service;

public class EmployeeService(BaseRepository<Employee, PIMDatabaseContext> repository) {
    private readonly BaseRepository<Employee, PIMDatabaseContext> _repository = repository;

    public async Task<ResponseEntity<EmployeeBaseResponse>> FindById (int id) {
        try {
            var result = await _repository.GetByIdAsync( id );
            return ResponseEntity<EmployeeBaseResponse>.CreateSuccess(new EmployeeBaseResponse {
                Id = result.Id,
                Visa = result.Visa,
                FirstName = result.FirstName,
                LastName = result.LastName,
                BirthDay = result.BirthDay,
                Version = result.Version
            });
        }catch (Exception) {
            return ResponseEntity<EmployeeBaseResponse>.Other("Not Found Employee With This ID", 200);
        }
    }

    public async Task<ResponseEntity<object>> InsertEmployee (EmployeeRequest employee) {
        Employee mapper = new() {
            Visa = employee.Visa,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDay = employee.BirthDay
        };
        
        try {
            await _repository.AddAsync(mapper);
            return ResponseEntity<object>.CreateSuccess(mapper);
        } catch (Exception ex) {
        return ResponseEntity<object>.Other(ex.Message, 200);
        }
    }
}