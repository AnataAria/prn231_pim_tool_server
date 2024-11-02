using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.DTO.Response;
using Service.Service;

namespace PIMToolServerAPI.Controller;

[ApiController, Route("/api/v1/projects"), Authorize]
public class ProjectController(ProjectService projectService): ControllerBase {
    private ProjectService service = projectService;
    public async Task<ActionResult<ResponseListEntity<ProjectBaseResponse>>> GetPage([FromQuery] int page, [FromQuery] int size) {
        var project = await service.GetProjects(page, size);
        return Ok(project);
    }
}