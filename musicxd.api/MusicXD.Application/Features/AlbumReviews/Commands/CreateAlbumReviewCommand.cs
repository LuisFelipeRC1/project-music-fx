using MediatR;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Domain.Entities;

namespace MusicXD.Application.Features.AlbumReviews.Commands;

public record CreateAlbumReviewCommand(Guid UserId, Guid AlbumId, decimal Rating, string Content) : IRequest<AlbumReviewDto>;

public class CreateAlbumReviewCommandHandler : IRequestHandler<CreateAlbumReviewCommand, AlbumReviewDto>
{
    private readonly IApplicationDbContext _context;

    public CreateAlbumReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AlbumReviewDto> Handle(CreateAlbumReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new AlbumReview
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            AlbumId = request.AlbumId,
            Rating = request.Rating,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.AlbumReviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return new AlbumReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            AlbumId = review.AlbumId,
            Rating = review.Rating,
            Content = review.Content,
            CreatedAt = review.CreatedAt
        };
    }
}
