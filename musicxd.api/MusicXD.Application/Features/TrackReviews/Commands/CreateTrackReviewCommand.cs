using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Application.Mapper;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;
using ActivityEntry = MusicXD.Domain.Entities.Activity;

namespace MusicXD.Application.Features.TrackRatings.Commands;

public record CreateTrackRatingCommand(Guid UserId, Guid TrackId, decimal Rating) : IRequest<TrackRatingDto>;

public class CreateTrackRatingCommandHandler : IRequestHandler<CreateTrackRatingCommand, TrackRatingDto>
{
    private readonly IApplicationDbContext _context;

    public CreateTrackRatingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TrackRatingDto> Handle(CreateTrackRatingCommand request, CancellationToken cancellationToken)
    {
        var alreadyRated = await _context.TrackRatings
            .AnyAsync(rating => rating.UserId == request.UserId && rating.TrackId == request.TrackId, cancellationToken);

        if (alreadyRated)
            throw new ArgumentException("A user can only rate the same track once.", nameof(request.TrackId));

        var rating = new TrackRating(
            request.UserId,
            request.TrackId,
            new Rating(request.Rating));

        _context.TrackRatings.Add(rating);
        _context.Activities.Add(new ActivityEntry(
            request.UserId,
            ActivityType.TrackRated,
            rating.Id.ToString()));
        await _context.SaveChangesAsync(cancellationToken);

        return rating.ToTrackRatingDto();
    }
}
