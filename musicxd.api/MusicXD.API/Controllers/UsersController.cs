using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Application.Features.ActivityFeed.Queries;
using MusicXD.Application.Features.Users.Commands;
using MusicXD.Application.Features.Users.Queries;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfile(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserProfileQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{id}/follow")]
    [Authorize]
    public async Task<IActionResult> Follow(Guid id, CancellationToken cancellationToken)
    {
        var followerId = GetUserId();
        await _mediator.Send(new FollowUserCommand(followerId, id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id}/feed")]
    [Authorize]
    public async Task<IActionResult> GetFeed(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetActivityFeedQuery(id), cancellationToken);
        return Ok(result);
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
            ?? User.FindFirst("sub")
            ?? throw new UnauthorizedAccessException("User not authenticated.");
        return Guid.Parse(claim.Value);
    }
}
