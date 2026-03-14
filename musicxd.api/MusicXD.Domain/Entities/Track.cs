namespace MusicXD.Domain.Entities;

public class Track
{
    public Guid Id { get; set; }
    public string SpotifyId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Guid AlbumId { get; set; }
    public Album Album { get; set; } = null!;
    public int DurationMs { get; set; }
    public int TrackNumber { get; set; }
}
