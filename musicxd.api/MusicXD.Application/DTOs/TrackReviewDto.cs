namespace MusicXD.Application.DTOs;

public class TrackRatingDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public Guid TrackId { get; set; }
    public decimal Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}
