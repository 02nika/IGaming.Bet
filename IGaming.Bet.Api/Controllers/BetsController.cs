using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Dto.Bet;

namespace IGaming.Bet.Solution.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class BetsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public BetsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
        
    [MapToApiVersion(1)]
    [HttpPost("place")]
    [Authorize]
    public async Task<IActionResult> PlaceBet([FromBody] BetDto betDto)
    {
        var authorization = HttpContext.Request.Headers["Authorization"];
        var tokenHash = authorization.First()!.Split(' ')[1];

        var userId = _serviceManager.AuthService.GetUserId(tokenHash);
        
        var winAmount = await _serviceManager.BetService.BetAsync(betDto, userId);
        
        return Ok(winAmount);
    }
}