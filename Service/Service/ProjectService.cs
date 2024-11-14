using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Service.DTO;
using Service.DTO.Response;

namespace Service.Service;

public class ProjectService(ProjectRepository projectRepository, EmployeeRepository employeeRepository)
{
    private readonly ProjectRepository repository = projectRepository;
    private readonly EmployeeRepository _employeeRepository = employeeRepository;

    public async Task<ResponseEntity<List<ProjectBaseResponse>>> SearchProjectsAsync(
        string searchTerm = "all",
        string status = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        try
        {
            var projectsQuery = await repository.FindByConditionWithPaginationAsync((p) =>
                (searchTerm == "all" ||
                 p.Name.Contains(searchTerm) ||
                 p.Customer.Contains(searchTerm)) &&
                (status == null || p.Status == status) &&
                (!startDate.HasValue || p.StartDate >= startDate) &&
                (!endDate.HasValue || p.EndDate <= endDate), pageNumber, pageSize);

            IEnumerable<ProjectBaseResponse> projectBaseResponses = projectsQuery.Select(p =>
            {
                return new ProjectBaseResponse
                {
                    GroupId = p.GroupId,
                    ProjectNumber = p.ProjectNumber,
                    Name = p.Name,
                    Customer = p.Customer,
                    Status = p.Status,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Version = p.Version,
                };
            });

            return ResponseEntity<List<ProjectBaseResponse>>.CreateSuccess(projectBaseResponses.ToList());
        }
        catch (Exception ex)
        {
            return ResponseEntity<List<ProjectBaseResponse>>.Other(ex.Message, 200);
        }
    }
    public async Task<ResponseEntity<ProjectBaseResponse>> FindById(long id)
    {
        try
        {
            var result = await repository.GetByProjectIdAsync(id);

            return ResponseEntity<ProjectBaseResponse>.CreateSuccess(new ProjectBaseResponse
            {
                GroupId = result.GroupId,
                ProjectNumber = result.ProjectNumber,
                Name = result.Name,
                Customer = result.Customer,
                Status = result.Status,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                Version = result.Version,
            });
        }
        catch (Exception)
        {
            return ResponseEntity<ProjectBaseResponse>.Other("Not Found Project With This ID", 200);
        }
    }
    public async Task<ResponseEntity<Object>> CreateProject(ProjectRequest projectRequest)
    {
        try
        {
            bool projectIDValid = await repository.IsProjectIdExisted(projectRequest.ProjectNumber);
            if (projectIDValid)
            {
                var entitySample = new Project()
                {
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
            } else {
                return ResponseEntity<Object>.Other("Project Number Matched", 200);
            }
        }
        catch (Exception ex)
        {
            return ResponseEntity<Object>.Other(ex.Message, 200);
        }
    }

    public async Task<ResponseEntity<Object>> UpdateProject(ProjectRequest projectRequest, int id)
    {
        try
        {
            var entity = await repository.GetByProjectIdAsync(id);
            entity.ProjectNumber = projectRequest.ProjectNumber;
            entity.GroupId = projectRequest.GroupId;
            entity.Status = projectRequest.Status;
            entity.Customer = projectRequest.Customer;
            entity.StartDate = projectRequest.StartDate;
            entity.EndDate = projectRequest.EndDate;
            entity.Name = projectRequest.Name;
            entity.Version++;
            await repository.AddAsync(entity);
            return ResponseEntity<Object>.CreateSuccess("Update Product Success");
        }
        catch (Exception ex)
        {
            return ResponseEntity<Object>.Other(ex.Message, 200);
        }
    }
    public async Task<ResponseEntity<Object>> RemoveProject(int id)
    {
        try
        {
            await repository.DeleteAsync(id);
            return ResponseEntity<Object>.CreateSuccess("Remove Project Success");
        }
        catch (Exception ex)
        {
            return ResponseEntity<Object>.Other(ex.Message, 200);
        }
    }
}