using DataAccessLayer.BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.DTO.Response;
using Service.Service;

namespace PIMToolServerAPI.Controller;

[ApiController, Route("/api/v1/groups")]
public class GroupController(GroupService groupService) : ControllerBase
{
    private readonly GroupService _groupService = groupService;

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseEntity<GroupResponseDto>>> GetGroupById([FromRoute] int id)
    {
        var response = await _groupService.GetGroupByIdAsync(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseEntity<GroupResponseDto>>> CreateGroup([FromBody] CreateGroupDto createGroupDto)
    {
        var response = await _groupService.CreateGroupAsync(createGroupDto);
        return CreatedAtAction(nameof(GetGroupById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseEntity<GroupResponseDto>>> UpdateGroup(int id, [FromBody] CreateGroupDto updateGroupDto)
    {
        var response = await _groupService.UpdateGroupAsync(id, updateGroupDto);
        if (response.StatusCode == 404)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseEntity<GroupResponseDto>>> DeleteGroup(int id)
    {
        try
        {
            var response = await _groupService.DeleteGroupAsync(id);
            if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<ResponseEntity<IEnumerable<GroupResponseDto>>>> GetAllGroupsAsync()
    {
        try
        {
            var response = await _groupService.GetAllGroupsAsync();
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,$"Server error: {ex.Message}" );
        }
    }
}