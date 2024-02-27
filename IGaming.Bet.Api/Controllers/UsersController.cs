using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Dto.Auth;
using Shared.Dto.User;

namespace IGaming.Bet.Solution.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public UsersController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [MapToApiVersion(1)]
    [HttpPost("register")]
    public async Task<IActionResult> AddUser([FromBody] AddUserDto userDto)
    {
        await _serviceManager.UserService.AddUserAsync(userDto);
        
        return Ok();
    }
    
    [MapToApiVersion(1)]
    [HttpPost("authentication")]
    public async Task<IActionResult> Authentication([FromBody] AuthRequest authRequest)
    {
        var u = await _serviceManager.UserService.GetUserAsync(authRequest);
        var response = _serviceManager.AuthService.Auth(u.Id);
        return Ok(response);
    }
    
    [MapToApiVersion(1)]
    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var authorization = HttpContext.Request.Headers["Authorization"];
        var tokenHash = authorization.First()!.Split(' ')[1];

        var userId = _serviceManager.AuthService.GetUserId(tokenHash);

        var userDto = await _serviceManager.UserService.GetUserAsync(userId);
        
        return Ok(userDto);
    }
}