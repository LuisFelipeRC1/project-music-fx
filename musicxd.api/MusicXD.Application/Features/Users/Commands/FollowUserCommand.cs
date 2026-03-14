using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicXD.Application.Interfaces;
using MusicXD.Domain.Entities;

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

        var follow = new Follow
        {
            Id = Guid.NewGuid(),
            FollowerId = request.FollowerId,
            FolloweeId = request.FolloweeId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Follows.Add(follow);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
