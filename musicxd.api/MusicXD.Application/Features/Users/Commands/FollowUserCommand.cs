using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicXD.Application.Interfaces;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using ActivityFeedEntry = MusicXD.Domain.Entities.ActivityFeed;

namespace MusicXD.Application.Features.Users.Commands;

public record FollowUserCommand(Guid FollowerId, Guid FolloweeId) : IRequest<Unit>;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public FollowUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var alreadyFollowing = await _context.Follows
            .AnyAsync(f => f.FollowerId == request.FollowerId && f.FolloweeId == request.FolloweeId, cancellationToken);

        if (alreadyFollowing)
            return Unit.Value;

        var follow = new Follow(request.FollowerId, request.FolloweeId);

        _context.Follows.Add(follow);
        _context.ActivityFeeds.Add(new ActivityFeedEntry(
            request.FollowerId,
            ActivityType.UserFollowed,
            request.FolloweeId.ToString()));
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
