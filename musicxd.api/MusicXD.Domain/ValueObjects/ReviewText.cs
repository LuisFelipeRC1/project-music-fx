namespace MusicXD.Domain.ValueObjects;

public sealed record ReviewText
{
    public string Value { get; }

    public ReviewText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Review text cannot be empty.", nameof(value));

        var normalized = value.Trim();

        if (normalized.Length > 5000)
            throw new ArgumentException("Review text cannot be longer than 5000 characters.", nameof(value));

        Value = normalized;
    }

    public override string ToString() => Value;
}
