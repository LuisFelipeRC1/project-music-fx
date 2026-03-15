namespace MusicXD.Application.DTOs;

public class TrackDto
{
    public Guid Id { get; set; }
    public string SpotifyId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Guid AlbumId { get; set; }
    public string AlbumTitle { get; set; } = string.Empty;
    public int DurationMs { get; set; }
    public int TrackNumber { get; set; }
}
