using MusicXD.Domain.Abstractions;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Entities;

public class Album : Entity
{
    public Guid Id { get; private set; }
    public SpotifyId SpotifyId { get; private set; } = null!;
    public string Title { get; private set; } = string.Empty;
    public Guid ArtistId { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string? CoverImageUrl { get; private set; }
    public MusicSource Source { get; private set; }
    public List<string> Genres { get; private set; } = new();
    public List<AlbumReview> Reviews { get; private set; } = new();
    public List<Track> Tracks { get; private set; } = new();

    public Album(
        SpotifyId spotifyId,
        string title,
        Guid artistId,
        DateTime releaseDate,
        string? coverImageUrl = null,
        IEnumerable<string>? genres = null,
        MusicSource source = MusicSource.Spotify)
    {
        Id = Guid.NewGuid();
        SpotifyId = spotifyId ?? throw new ArgumentNullException(nameof(spotifyId));
        Title = ValidateRequired(title, nameof(title), 500);
        ArtistId = ValidateForeignKey(artistId, nameof(artistId));
        ReleaseDate = releaseDate;
        CoverImageUrl = NormalizeOptional(coverImageUrl, nameof(coverImageUrl), 2048);
        Genres = NormalizeGenres(genres);
        Source = source;
    }

    public void UpdateMetadata(string title, DateTime releaseDate, string? coverImageUrl, IEnumerable<string>? genres, MusicSource source)
    {
        Title = ValidateRequired(title, nameof(title), 500);
        ReleaseDate = releaseDate;
        CoverImageUrl = NormalizeOptional(coverImageUrl, nameof(coverImageUrl), 2048);
        Genres = NormalizeGenres(genres);
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

    private static string? NormalizeOptional(string? value, string paramName, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var normalized = value.Trim();

        if (normalized.Length > maxLength)
            throw new ArgumentException($"{paramName} cannot be longer than {maxLength} characters.", paramName);

        return normalized;
    }

    private static List<string> NormalizeGenres(IEnumerable<string>? genres)
    {
        return genres?
            .Where(genre => !string.IsNullOrWhiteSpace(genre))
            .Select(genre => genre.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList() ?? new List<string>();
    }

    private Album() { }
}
