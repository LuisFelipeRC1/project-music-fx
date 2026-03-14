namespace MusicXD.Application.DTOs;

public class AlbumDto
{
    public Guid Id { get; set; }
    public string SpotifyId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Guid ArtistId { get; set; }
    public string ArtistName { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string? CoverImageUrl { get; set; }
    public List<string> Genres { get; set; } = new();
}
