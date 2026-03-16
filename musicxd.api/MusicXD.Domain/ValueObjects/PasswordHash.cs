namespace MusicXD.Domain.ValueObjects;

public sealed record PasswordHash
{
    public string Value { get; }

    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password hash cannot be empty.", nameof(value));

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
