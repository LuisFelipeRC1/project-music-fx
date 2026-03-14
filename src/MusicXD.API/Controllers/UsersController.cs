using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Application.Common.Interfaces;
using MusicXD.Application.DTOs;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetProfile(Guid userId, CancellationToken cancellationToken)
    {
        var profile = await _userService.GetProfileAsync(userId, cancellationToken);
        return Ok(profile);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var profile = await _userService.UpdateProfileAsync(userId, request, cancellationToken);
        return Ok(profile);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q, CancellationToken cancellationToken)
    {
        var results = await _userService.SearchUsersAsync(q, cancellationToken);
        return Ok(results);
    }

    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        return Guid.Parse(claim!);
    }
}
