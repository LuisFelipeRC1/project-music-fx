namespace MusicXD.Domain.Entities;

public class Album
{
    public Guid AlbumId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public Guid ArtistId { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string? SpotifyId { get; private set; }
    public string? CoverUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Artist Artist { get; private set; } = null!;
    public ICollection<Track> Tracks { get; private set; } = new List<Track>();
    public ICollection<Review> Reviews { get; private set; } = new List<Review>();

    private Album() { }

    public static Album Create(string title, Guid artistId, DateTime releaseDate, string? spotifyId = null, string? coverUrl = null)
    {
        return new Album
        {
            AlbumId = Guid.NewGuid(),
            Title = title,
            ArtistId = artistId,
            ReleaseDate = releaseDate,
            SpotifyId = spotifyId,
            CoverUrl = coverUrl,
            CreatedAt = DateTime.UtcNow
        };
    }
}
