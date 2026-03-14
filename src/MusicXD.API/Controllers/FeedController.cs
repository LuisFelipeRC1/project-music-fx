using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Application.Common.Interfaces;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FeedController : ControllerBase
{
    private readonly IActivityFeedService _feedService;

    public FeedController(IActivityFeedService feedService)
    {
        _feedService = feedService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFeed(CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var feed = await _feedService.GetFeedAsync(userId, cancellationToken);
        return Ok(feed);
    }

    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        return Guid.Parse(claim!);
    }
}
