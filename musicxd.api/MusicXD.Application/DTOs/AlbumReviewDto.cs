namespace MusicXD.Application.DTOs;

public class AlbumReviewDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public Guid AlbumId { get; set; }
    public decimal Rating { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
