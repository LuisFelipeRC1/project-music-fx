namespace MusicXD.Domain.Entities;

public class AlbumReview
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid AlbumId { get; set; }
    public Album Album { get; set; } = null!;
    public decimal Rating { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
