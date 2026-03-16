namespace MusicXD.Domain.ValueObjects;

public sealed record SpotifyId
{
    public string Value { get; }

    public SpotifyId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SpotifyId cannot be empty.", nameof(value));

        var normalized = value.Trim();

        if (normalized.Length > 100)
            throw new ArgumentException("SpotifyId cannot be longer than 100 characters.", nameof(value));

        Value = normalized;
    }

    public override string ToString() => Value;
}
