namespace MusicXD.Domain.Entities;

public class Album
{
    public Guid Id { get; set; }
    public string SpotifyId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public string? CoverImageUrl { get; set; }
    public List<string> Genres { get; set; } = new();
}
