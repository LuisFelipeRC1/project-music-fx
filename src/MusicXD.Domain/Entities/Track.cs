namespace MusicXD.Domain.Entities;

public class Track
{
    public Guid TrackId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid AlbumId { get; private set; }
    public int Duration { get; private set; }
    public string? SpotifyId { get; private set; }
    public int TrackNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Album Album { get; private set; } = null!;
    public ICollection<TrackRating> Ratings { get; private set; } = new List<TrackRating>();

    private Track() { }

    public static Track Create(string name, Guid albumId, int duration, string? spotifyId = null, int trackNumber = 0)
    {
        return new Track
        {
            TrackId = Guid.NewGuid(),
            Name = name,
            AlbumId = albumId,
            Duration = duration,
            SpotifyId = spotifyId,
            TrackNumber = trackNumber,
            CreatedAt = DateTime.UtcNow
        };
    }
}
