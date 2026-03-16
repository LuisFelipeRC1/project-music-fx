using System.Text.RegularExpressions;

namespace MusicXD.Domain.ValueObjects;

public sealed partial record Username
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Username cannot be empty.", nameof(value));

        var normalized = value.Trim().ToLowerInvariant();

        if (!UsernameRegex().IsMatch(normalized))
            throw new ArgumentException("Username must be 3-50 characters and contain only letters, numbers, or underscores.", nameof(value));

        Value = normalized;
    }

    public override string ToString() => Value;

    [GeneratedRegex("^[a-z0-9_]{3,50}$", RegexOptions.Compiled)]
    private static partial Regex UsernameRegex();
}
