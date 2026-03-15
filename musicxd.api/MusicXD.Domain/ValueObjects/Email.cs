namespace MusicXD.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));

        var normalized = value.Trim().ToLowerInvariant();

        if (!IsValidEmail(normalized))
            throw new ArgumentException("Invalid email format.", nameof(value));

        Value = normalized;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public override string ToString() => Value;
}
