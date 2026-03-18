using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Application.Mapper;

namespace MusicXD.Application.Features.Users.Queries;

public record GetUserProfileQuery(Guid UserId) : IRequest<UserDto>;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserDto>
{
    private readonly IApplicationDbContext _context;

    public GetUserProfileQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
            ?? throw new KeyNotFoundException($"User {request.UserId} not found.");

        return user.ToUserDto();
    }
}
