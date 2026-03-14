using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Application.Common.Interfaces;
using MusicXD.Application.DTOs;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FollowsController : ControllerBase
{
    private readonly IFollowService _followService;

    public FollowsController(IFollowService followService)
    {
        _followService = followService;
    }

    [HttpPost]
    public async Task<IActionResult> Follow([FromBody] FollowRequest request, CancellationToken cancellationToken)
    {
        var followerId = GetCurrentUserId();
        await _followService.FollowAsync(followerId, request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{followingId:guid}")]
    public async Task<IActionResult> Unfollow(Guid followingId, CancellationToken cancellationToken)
    {
        var followerId = GetCurrentUserId();
        await _followService.UnfollowAsync(followerId, followingId, cancellationToken);
        return NoContent();
    }

    [HttpGet("followers/{userId:guid}")]
    public async Task<IActionResult> GetFollowers(Guid userId, CancellationToken cancellationToken)
    {
        var followers = await _followService.GetFollowersAsync(userId, cancellationToken);
        return Ok(followers);
    }

    [HttpGet("following/{userId:guid}")]
    public async Task<IActionResult> GetFollowing(Guid userId, CancellationToken cancellationToken)
    {
        var following = await _followService.GetFollowingAsync(userId, cancellationToken);
        return Ok(following);
    }

    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        return Guid.Parse(claim!);
    }
}
