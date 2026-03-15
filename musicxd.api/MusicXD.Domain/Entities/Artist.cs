namespace MusicXD.Domain.Entities;

public class Artist
{
    public Guid Id { get; private set; }
    public string SpotifyId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    public List<string> Genres { get; private set; } = new();

    public Artist(string spotifyId, string name, string? imageUrl = null, IEnumerable<string>? genres = null)
    {
        Id = Guid.NewGuid();
        SpotifyId = ValidateRequired(spotifyId, nameof(spotifyId), 100);
        Name = ValidateRequired(name, nameof(name), 500);
        ImageUrl = NormalizeOptional(imageUrl, nameof(imageUrl), 2048);
        Genres = NormalizeGenres(genres);
    }

    public void UpdateCatalogDetails(string name, string? imageUrl, IEnumerable<string>? genres)
    {
        Name = ValidateRequired(name, nameof(name), 500);
        ImageUrl = NormalizeOptional(imageUrl, nameof(imageUrl), 2048);
        Genres = NormalizeGenres(genres);
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
