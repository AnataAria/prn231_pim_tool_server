using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.DTO.Response;
using Service.Service;

namespace PIMToolServerAPI.Controller;

[ApiController, Route("/api/v1/projects"), Authorize]
public class ProjectController(ProjectService projectService): ControllerBase {
    private readonly ProjectService service = projectService;
    [HttpGet]
    public async Task<ActionResult<ResponseListEntity<ProjectBaseResponse>>> GetPage([FromQuery] int page = 1, [FromQuery] int size = 5) {
        var project = await service.GetProjects(page, size);
        return Ok(project);
    }
}