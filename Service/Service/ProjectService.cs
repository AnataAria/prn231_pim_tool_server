using DataAccessLayer;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Service.DTO;
using Service.DTO.Response;

namespace Service.Service;

public class ProjectService(BaseRepository<Project, PIMDatabaseContext> repo)
{
    private readonly BaseRepository<Project, PIMDatabaseContext> repository = repo;

    public async Task<ResponseListEntity<ProjectBaseResponse>> GetProjects(int page, int size)
    {
        var result = await repository.GetPage(page, size);
        int totalElement = await repository.CountAsync();
        ICollection<ProjectBaseResponse> mapResult = result.Select(x => new ProjectBaseResponse { }).ToList();
        return ResponseListEntity<ProjectBaseResponse>.CreateSuccess(mapResult,
        page,
        size,
        (int)Math.Ceiling((double)totalElement / size),
        totalElement);
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