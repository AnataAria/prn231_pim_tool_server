using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Request;
using Service.DTO.Response;
using Service.Service;

namespace PIMToolServerAPI.Controller;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    private readonly AuthService authService = authService;
    [HttpPost("authentication")]
    public async Task<ActionResult<ResponseEntity<AuthenticationResponse>>> Login([FromBody] AuthenticationRequest request)
    {
        var response = await authService.Login(request);
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet("profile"), Authorize]
    public IActionResult GetUserProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = User.FindFirst(ClaimTypes.Name)?.Value;

        return Ok(
            ResponseEntity<object>.CreateSuccess(new
            {
                UserId = userId,
                Username = username
            })
        );
    }
}