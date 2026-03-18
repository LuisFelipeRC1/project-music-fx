using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Application.Mapper;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;
using ActivityEntry = MusicXD.Domain.Entities.Activity;

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
        var alreadyReviewed = await _context.AlbumReviews
            .AnyAsync(review => review.UserId == request.UserId && review.AlbumId == request.AlbumId, cancellationToken);

        if (alreadyReviewed)
            throw new ArgumentException("A user can only review the same album once.");

        var review = new AlbumReview(
            request.UserId,
            request.AlbumId,
            new Rating(request.Rating),
            new ReviewText(request.Content));

        _context.AlbumReviews.Add(review);
        _context.Activities.Add(new ActivityEntry(
            request.UserId,
            ActivityType.AlbumReviewed,
            review.Id.ToString()));
        await _context.SaveChangesAsync(cancellationToken);

        return review.ToAlbumReviewDto();
    }
}
