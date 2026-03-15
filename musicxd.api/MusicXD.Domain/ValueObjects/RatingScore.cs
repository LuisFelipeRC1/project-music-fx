using System.Globalization;

namespace MusicXD.Domain.ValueObjects;

public sealed record RatingScore
{
    public decimal Value { get; }

    public RatingScore(decimal value)
    {
        if (value < 0m || value > 5m)
            throw new ArgumentOutOfRangeException(nameof(value), "Rating must be between 0 and 5.");

        Value = decimal.Round(value, 1, MidpointRounding.AwayFromZero);
    }

    public override string ToString() => Value.ToString("0.0", CultureInfo.InvariantCulture);
}
