namespace MusicXD.Domain.Entities;

public class Artist
{
    public Guid ArtistId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? SpotifyId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ICollection<Album> Albums { get; private set; } = new List<Album>();

    private Artist() { }

    public static Artist Create(string name, string? spotifyId = null)
    {
        return new Artist
        {
            ArtistId = Guid.NewGuid(),
            Name = name,
            SpotifyId = spotifyId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
