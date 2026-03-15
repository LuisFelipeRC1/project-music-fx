using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Application.Features.TrackReviews.Commands;
using MusicXD.Application.Features.TrackReviews.Queries;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/tracks/{trackId}/reviews")]
public class TrackReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrackReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews(Guid trackId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTrackReviewsQuery(trackId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateReview(Guid trackId, [FromBody] CreateTrackReviewRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var result = await _mediator.Send(new CreateTrackReviewCommand(userId, trackId, request.Rating, request.Content), cancellationToken);
        return CreatedAtAction(nameof(GetReviews), new { trackId }, result);
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
            ?? User.FindFirst("sub")
            ?? throw new UnauthorizedAccessException("User not authenticated.");
        return Guid.Parse(claim.Value);
    }
}

public record CreateTrackReviewRequest(decimal Rating, string Content);
