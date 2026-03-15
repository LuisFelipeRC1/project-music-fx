namespace MusicXD.Domain.Entities;

public class Artist
{
    public Guid Id { get; set; }
    public string SpotifyId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public List<string> Genres { get; set; } = new();
}
