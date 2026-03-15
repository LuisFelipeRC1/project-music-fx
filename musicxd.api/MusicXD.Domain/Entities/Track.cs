namespace MusicXD.Domain.Entities;

public class Track
{
    public Guid Id { get; private set; }
    public string SpotifyId { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public Guid AlbumId { get; private set; }
    public int DurationMs { get; private set; }
    public int TrackNumber { get; private set; }

    public Track(string spotifyId, string title, Guid albumId, int durationMs, int trackNumber)
    {
        Id = Guid.NewGuid();
        SpotifyId = ValidateRequired(spotifyId, nameof(spotifyId), 100);
        Title = ValidateRequired(title, nameof(title), 500);
        AlbumId = ValidateForeignKey(albumId, nameof(albumId));
        DurationMs = ValidatePositiveNumber(durationMs, nameof(durationMs));
        TrackNumber = ValidatePositiveNumber(trackNumber, nameof(trackNumber));
    }

    public void UpdateMetadata(string title, int durationMs, int trackNumber)
    {
        Title = ValidateRequired(title, nameof(title), 500);
        DurationMs = ValidatePositiveNumber(durationMs, nameof(durationMs));
        TrackNumber = ValidatePositiveNumber(trackNumber, nameof(trackNumber));
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
