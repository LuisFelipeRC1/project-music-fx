using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Application.Features.TrackRatings.Commands;
using MusicXD.Application.Features.TrackRatings.Queries;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/tracks/{trackId}/ratings")]
[Route("api/tracks/{trackId}/reviews")]
public class TrackRatingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrackRatingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRatings(Guid trackId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTrackRatingsQuery(trackId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateRating(Guid trackId, [FromBody] CreateTrackRatingRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var result = await _mediator.Send(new CreateTrackRatingCommand(userId, trackId, request.Rating), cancellationToken);
        return CreatedAtAction(nameof(GetRatings), new { trackId }, result);
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
            ?? User.FindFirst("sub")
            ?? throw new UnauthorizedAccessException("User not authenticated.");
        return Guid.Parse(claim.Value);
    }
}

public record CreateTrackRatingRequest(decimal Rating);
