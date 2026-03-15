using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Entities;

public class AlbumReview
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid AlbumId { get; private set; }
    public RatingScore Rating { get; private set; } = null!;
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public AlbumReview(Guid userId, Guid albumId, RatingScore rating, string content)
    {
        Id = Guid.NewGuid();
        UserId = ValidateForeignKey(userId, nameof(userId));
        AlbumId = ValidateForeignKey(albumId, nameof(albumId));
        Rating = rating ?? throw new ArgumentNullException(nameof(rating));
        Content = ValidateContent(content);
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void UpdateReview(RatingScore rating, string content)
    {
        Rating = rating ?? throw new ArgumentNullException(nameof(rating));
        Content = ValidateContent(content);
        UpdatedAt = DateTime.UtcNow;
    }

    private static Guid ValidateForeignKey(Guid value, string paramName)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);

        return value;
    }

    private static string ValidateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("content cannot be empty.", nameof(content));

        var normalized = content.Trim();

        if (normalized.Length > 5000)
            throw new ArgumentException("content cannot be longer than 5000 characters.", nameof(content));

        return normalized;
    }

    private AlbumReview() { }
}
