using MusicXD.Domain.Abstractions;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Entities;

public class Track : Entity
{
    public Guid Id { get; private set; }
    public SpotifyId SpotifyId { get; private set; } = null!;
    public string Title { get; private set; } = string.Empty;
    public Guid AlbumId { get; private set; }
    public int DurationMs { get; private set; }
    public int TrackNumber { get; private set; }
    public MusicSource Source { get; private set; }
    public List<TrackRating> Ratings { get; private set; } = new();

    public Track(SpotifyId spotifyId, string title, Guid albumId, int durationMs, int trackNumber, MusicSource source = MusicSource.Spotify)
    {
        Id = Guid.NewGuid();
        SpotifyId = spotifyId ?? throw new ArgumentNullException(nameof(spotifyId));
        Title = ValidateRequired(title, nameof(title), 500);
        AlbumId = ValidateForeignKey(albumId, nameof(albumId));
        DurationMs = ValidatePositiveNumber(durationMs, nameof(durationMs));
        TrackNumber = ValidatePositiveNumber(trackNumber, nameof(trackNumber));
        Source = source;
    }

    public void UpdateMetadata(string title, int durationMs, int trackNumber, MusicSource source)
    {
        Title = ValidateRequired(title, nameof(title), 500);
        DurationMs = ValidatePositiveNumber(durationMs, nameof(durationMs));
        TrackNumber = ValidatePositiveNumber(trackNumber, nameof(trackNumber));
        Source = source;
    }

    private static Guid ValidateForeignKey(Guid value, string paramName)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);

        return value;
    }

    private static string ValidateRequired(string value, string paramName, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);

        var normalized = value.Trim();

        if (normalized.Length > maxLength)
            throw new ArgumentException($"{paramName} cannot be longer than {maxLength} characters.", paramName);

        return normalized;
    }

    private static int ValidatePositiveNumber(int value, string paramName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be greater than zero.");

        return value;
    }

    private Track() { }
}
