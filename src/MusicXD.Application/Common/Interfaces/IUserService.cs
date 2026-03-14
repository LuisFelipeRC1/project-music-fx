using MusicXD.Application.DTOs;

namespace MusicXD.Application.Common.Interfaces;

public interface IUserService
{
    Task<UserProfileResponse> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UserProfileResponse> UpdateProfileAsync(Guid userId, UpdateProfileRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserProfileResponse>> SearchUsersAsync(string query, CancellationToken cancellationToken = default);
}
