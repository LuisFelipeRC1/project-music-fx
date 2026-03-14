using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Application.Features.AlbumReviews.Commands;
using MusicXD.Application.Features.AlbumReviews.Queries;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/albums/{albumId}/reviews")]
public class AlbumReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AlbumReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews(Guid albumId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAlbumReviewsQuery(albumId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateReview(Guid albumId, [FromBody] CreateAlbumReviewRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var result = await _mediator.Send(new CreateAlbumReviewCommand(userId, albumId, request.Rating, request.Content), cancellationToken);
        return CreatedAtAction(nameof(GetReviews), new { albumId }, result);
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
            ?? User.FindFirst("sub")
            ?? throw new UnauthorizedAccessException("User not authenticated.");
        return Guid.Parse(claim.Value);
    }
}

public record CreateAlbumReviewRequest(decimal Rating, string Content);
