using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.DTO.Response;
using Service.Service;

namespace PIMToolServerAPI.Controller;

[ApiController, Route("/api/v1/projects")]
public class ProjectController(ProjectService projectService): ControllerBase {
    private readonly ProjectService service = projectService;
    [HttpGet]
    public async Task<ActionResult<List<ProjectBaseResponse>>> SearchProjects(
            [FromQuery] string searchTerm = "all",
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
    {
        try
        {
            var projects = await service.SearchProjectsAsync(searchTerm, startDate, endDate, pageNumber, pageSize);
            return Ok(projects);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that might occur
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
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