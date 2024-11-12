using DataAccessLayer;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Service.DTO;
using Service.DTO.Response;
using System.Linq;
using System.Linq.Expressions;

namespace Service.Service;

public class ProjectService(ProjectRepository projectRepository, EmployeeRepository employeeRepository)
{
    private readonly ProjectRepository repository = projectRepository;
    private readonly EmployeeRepository _employeeRepository = employeeRepository;

    public async Task<List<ProjectBaseResponse>> SearchProjectsAsync(
    string searchTerm = "all",
    string status = null,  // Make status nullable
    DateTime? startDate = null,
    DateTime? endDate = null,
    int pageNumber = 1,
    int pageSize = 10)
    {
        Expression<Func<Project, bool>> predicate = p =>
            (searchTerm == "all" ||
             p.Name.Contains(searchTerm) ||
             p.Customer.Contains(searchTerm)) &&
            (status == null || p.Status == status) &&  
            (!startDate.HasValue || p.StartDate >= startDate) &&
            (!endDate.HasValue || p.EndDate <= endDate);

        var projectsQuery = await repository.FindByConditionWithPaginationAsync(predicate, pageNumber, pageSize);

        var projectBaseResponses = new List<ProjectBaseResponse>();
        foreach (var p in projectsQuery)
        {
            var leader = await _employeeRepository.GetByIdAsync(p.GroupProject.LeaderId); 
            projectBaseResponses.Add(new ProjectBaseResponse
            {
                LeaderName = leader?.LastName,  
                ProjectNumber = p.ProjectNumber,
                Name = p.Name,
                Customer = p.Customer,
                Status = p.Status,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Version = p.Version,
            });
        }

        return projectBaseResponses;
    }





    public async Task<ResponseEntity<Object>> CreateProject (ProjectRequest projectRequest) {
        try {
            var entitySample = new Project() {
            GroupId = projectRequest.GroupId,
            Name = projectRequest.Name,
            Customer = projectRequest.Customer,
            Status = projectRequest.Status,
            StartDate = projectRequest.StartDate,
            EndDate = projectRequest.EndDate,
            ProjectNumber = projectRequest.ProjectNumber,
            Version = 1
        };
            await repository.AddAsync(entitySample);
            return ResponseEntity<Object>.CreateSuccess("Create Project Success");
        } catch(Exception ex) {
            return ResponseEntity<Object>.Other(ex.Message, 200);
        }
    }

    public async Task<ResponseEntity<Object>> UpdateProject (ProjectRequest projectRequest, int id) {
        try {
            var entity = await repository.GetByIdAsync(id);
            entity.ProjectNumber = projectRequest.ProjectNumber;
            entity.GroupId = projectRequest.GroupId;
            entity.Status = projectRequest.Status;
            entity.Customer = projectRequest.Customer;
            entity.StartDate = projectRequest.StartDate;
            entity.EndDate =   projectRequest.EndDate;
            entity.Name = projectRequest.Name;
            entity.Version ++;
            await repository.AddAsync(entity);
            return ResponseEntity<Object>.CreateSuccess("Update Product Success");
        } catch(Exception ex) {
            return ResponseEntity<Object>.Other(ex.Message, 200);
        }
    }
    public async Task<ResponseEntity<Object>> RemoveProject (int id) {
        try {
            await repository.DeleteAsync(id);
            return ResponseEntity<Object>.CreateSuccess("Remove Project Success");
        } catch(Exception ex) {
            return ResponseEntity<Object>.Other(ex.Message, 200);
        }
    }
}