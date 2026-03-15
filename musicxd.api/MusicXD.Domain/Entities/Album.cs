namespace MusicXD.Domain.Entities;

public class Album
{
    public Guid Id { get; private set; }
    public string SpotifyId { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public Guid ArtistId { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string? CoverImageUrl { get; private set; }
    public List<string> Genres { get; private set; } = new();

    public Album(
        string spotifyId,
        string title,
        Guid artistId,
        DateTime releaseDate,
        string? coverImageUrl = null,
        IEnumerable<string>? genres = null)
    {
        Id = Guid.NewGuid();
        SpotifyId = ValidateRequired(spotifyId, nameof(spotifyId), 100);
        Title = ValidateRequired(title, nameof(title), 500);
        ArtistId = ValidateForeignKey(artistId, nameof(artistId));
        ReleaseDate = releaseDate;
        CoverImageUrl = NormalizeOptional(coverImageUrl, nameof(coverImageUrl), 2048);
        Genres = NormalizeGenres(genres);
    }

    public void UpdateMetadata(string title, DateTime releaseDate, string? coverImageUrl, IEnumerable<string>? genres)
    {
        Title = ValidateRequired(title, nameof(title), 500);
        ReleaseDate = releaseDate;
        CoverImageUrl = NormalizeOptional(coverImageUrl, nameof(coverImageUrl), 2048);
        Genres = NormalizeGenres(genres);
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
