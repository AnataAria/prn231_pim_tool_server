using DataAccessLayer.BusinessObject;
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
    public async Task<ActionResult<ResponseEntity<List<ProjectBaseResponse>>>> SearchProjects(
        [FromQuery] string searchTerm = "all",
        [FromQuery] string status = null,  // Added nullable status
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var projects = await service.SearchProjectsAsync(searchTerm, status, startDate, endDate, pageNumber, pageSize);
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseEntity<ProjectBaseResponse>>> GetProjectById([FromRoute] int id)
    {
        var response = await service.FindById(id);
        return Ok(response);
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