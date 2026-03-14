using MusicXD.Domain.Entities;

namespace MusicXD.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
