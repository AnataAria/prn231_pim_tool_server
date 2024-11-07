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

    [HttpPost]
    public async Task<ActionResult<ResponseEntity<Object>>> CreateProject ([FromBody] ProjectRequest project) {
        var result = await service.CreateProject(project);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseEntity<Object>>> UpdateProject ([FromBody] ProjectRequest project, [FromRoute] int id) {
        var result = await service.UpdateProject(project, id);
        return StatusCode(result.StatusCode, result);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseEntity<Object>>> RemoveProject ([FromRoute] int id) {
        var result = await service.RemoveProject(id);
        return StatusCode(result.StatusCode, result);
    }
}