using MusicXD.Domain.Abstractions;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Entities;

public class Artist : Entity
{
    public Guid Id { get; private set; }
    public SpotifyId SpotifyId { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    public MusicSource Source { get; private set; }
    public List<string> Genres { get; private set; } = new();
    public List<Album> Albums { get; private set; } = new();

    public Artist(SpotifyId spotifyId, string name, string? imageUrl = null, IEnumerable<string>? genres = null, MusicSource source = MusicSource.Spotify)
    {
        Id = Guid.NewGuid();
        SpotifyId = spotifyId ?? throw new ArgumentNullException(nameof(spotifyId));
        Name = ValidateRequired(name, nameof(name), 500);
        ImageUrl = NormalizeOptional(imageUrl, nameof(imageUrl), 2048);
        Genres = NormalizeGenres(genres);
        Source = source;
    }

    public void UpdateCatalogDetails(string name, string? imageUrl, IEnumerable<string>? genres, MusicSource source)
    {
        Name = ValidateRequired(name, nameof(name), 500);
        ImageUrl = NormalizeOptional(imageUrl, nameof(imageUrl), 2048);
        Genres = NormalizeGenres(genres);
        Source = source;
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

    private Artist() { }
}
