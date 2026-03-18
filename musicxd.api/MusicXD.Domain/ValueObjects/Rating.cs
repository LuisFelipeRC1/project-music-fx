using System.Globalization;

namespace MusicXD.Domain.ValueObjects;

public sealed record Rating
{
    public decimal Value { get; }

    public Rating(decimal value)
    {
        var normalized = decimal.Round(value, 1, MidpointRounding.AwayFromZero);

        if (normalized < 1m || normalized > 5m)
            throw new ArgumentOutOfRangeException(nameof(value), "Rating must be between 1 and 5.");

        Value = normalized;
    }

    public override string ToString() => Value.ToString("0.0", CultureInfo.InvariantCulture);
}
