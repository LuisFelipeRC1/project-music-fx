namespace MusicXD.API.Contracts;

public sealed class SpotifyImageDto
{
    public string Url { get; set; } = string.Empty;
    public int? Height { get; set; }
    public int? Width { get; set; }
}

public sealed class SpotifyArtistSummaryDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public sealed class SpotifyAlbumSummaryDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<SpotifyImageDto> Images { get; set; } = new();
}

public sealed class SpotifyArtistDetailsDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> Genres { get; set; } = new();
    public List<SpotifyImageDto> Images { get; set; } = new();
}

public sealed class SpotifyAlbumDetailsDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ReleaseDate { get; set; }
    public string? ReleaseDatePrecision { get; set; }
    public List<SpotifyImageDto> Images { get; set; } = new();
    public List<SpotifyArtistSummaryDto> Artists { get; set; } = new();
    public List<string> Genres { get; set; } = new();
}

public sealed class SpotifyTrackDetailsDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int DurationMs { get; set; }
    public int TrackNumber { get; set; }
    public SpotifyAlbumSummaryDto? Album { get; set; }
    public List<SpotifyArtistSummaryDto> Artists { get; set; } = new();
}

public sealed class SpotifyArtistDto
{
    public SpotifyArtistDetailsDto Artist { get; set; } = new();
}

public sealed class SpotifyAlbumDto
{
    public SpotifyAlbumDetailsDto Album { get; set; } = new();
}

public sealed class SpotifyTrackDto
{
    public SpotifyTrackDetailsDto Track { get; set; } = new();
}

public sealed class SpotifyArtistSearchResultDto
{
    public List<SpotifyArtistDetailsDto> Result { get; set; } = new();
}

public sealed class SpotifyAlbumSearchResultDto
{
    public List<SpotifyAlbumDetailsDto> Result { get; set; } = new();
}

public sealed class SpotifyTrackSearchResultDto
{
    public List<SpotifyTrackDetailsDto> Result { get; set; } = new();
}
