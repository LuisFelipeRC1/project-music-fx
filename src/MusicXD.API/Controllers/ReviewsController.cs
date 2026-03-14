using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Application.Common.Interfaces;
using MusicXD.Application.DTOs;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost("albums")]
    public async Task<IActionResult> CreateAlbumReview([FromBody] CreateReviewRequest request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var review = await _reviewService.CreateReviewAsync(userId, request, cancellationToken);
        return CreatedAtAction(nameof(GetAlbumReviews), new { albumId = review.AlbumId }, review);
    }

    [HttpGet("albums/{albumId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAlbumReviews(Guid albumId, CancellationToken cancellationToken)
    {
        var reviews = await _reviewService.GetAlbumReviewsAsync(albumId, cancellationToken);
        return Ok(reviews);
    }

    [HttpPost("tracks")]
    public async Task<IActionResult> RateTrack([FromBody] CreateTrackRatingRequest request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var rating = await _reviewService.RateTrackAsync(userId, request, cancellationToken);
        return Ok(rating);
    }

    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        return Guid.Parse(claim!);
    }
}
