using DataAccessLayer;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Service.DTO;
using Service.DTO.Response;

namespace Service.Service;

public class ProjectService(BaseRepository<Project, PIMDatabaseContext> repo)
{
    private BaseRepository<Project, PIMDatabaseContext> repository = repo;

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
}