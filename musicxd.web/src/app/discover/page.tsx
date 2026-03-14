import AlbumCard from "@/components/AlbumCard";
import TrackCard from "@/components/TrackCard";
import { Album, Artist, Track } from "@/types";

const artist1: Artist = { id: "a1", name: "Tame Impala", followersCount: 5200000 };
const artist2: Artist = { id: "a2", name: "Radiohead", followersCount: 7800000 };
const artist3: Artist = { id: "a3", name: "Frank Ocean", followersCount: 9100000 };
const artist4: Artist = { id: "a4", name: "Kendrick Lamar", followersCount: 11000000 };
const artist5: Artist = { id: "a5", name: "Lana Del Rey", followersCount: 8400000 };
const artist6: Artist = { id: "a6", name: "Bon Iver", followersCount: 4300000 };

const topAlbums: Album[] = [
  { id: "alb1", title: "Currents", artist: artist1, coverUrl: "https://picsum.photos/seed/currents/300/300", releaseYear: 2015, tracksCount: 13, averageRating: 4.6, ratingsCount: 12400 },
  { id: "alb2", title: "OK Computer", artist: artist2, coverUrl: "https://picsum.photos/seed/okcomputer/300/300", releaseYear: 1997, tracksCount: 12, averageRating: 4.8, ratingsCount: 22100 },
  { id: "alb3", title: "Blonde", artist: artist3, coverUrl: "https://picsum.photos/seed/blonde/300/300", releaseYear: 2016, tracksCount: 17, averageRating: 4.7, ratingsCount: 18300 },
  { id: "alb4", title: "To Pimp a Butterfly", artist: artist4, coverUrl: "https://picsum.photos/seed/tpab/300/300", releaseYear: 2015, tracksCount: 16, averageRating: 4.9, ratingsCount: 24500 },
  { id: "alb5", title: "Norman Fucking Rockwell!", artist: artist5, coverUrl: "https://picsum.photos/seed/nfr/300/300", releaseYear: 2019, tracksCount: 14, averageRating: 4.5, ratingsCount: 9800 },
  { id: "alb6", title: "Bon Iver, Bon Iver", artist: artist6, coverUrl: "https://picsum.photos/seed/boniver/300/300", releaseYear: 2011, tracksCount: 10, averageRating: 4.4, ratingsCount: 7200 },
];

const mockAlbum1 = topAlbums[0];
const mockAlbum2 = topAlbums[2];

const topTracks: Track[] = [
  { id: "t1", title: "Let It Happen", album: mockAlbum1, artist: artist1, durationMs: 467000, trackNumber: 1, averageRating: 4.8, ratingsCount: 8200 },
  { id: "t2", title: "Paranoid Android", album: topAlbums[1], artist: artist2, durationMs: 386000, trackNumber: 2, averageRating: 4.9, ratingsCount: 15400 },
  { id: "t3", title: "Nights", album: mockAlbum2, artist: artist3, durationMs: 307000, trackNumber: 10, averageRating: 4.7, ratingsCount: 11200 },
  { id: "t4", title: "Alright", album: topAlbums[3], artist: artist4, durationMs: 215000, trackNumber: 8, averageRating: 4.8, ratingsCount: 13100 },
  { id: "t5", title: "Mariners Apartment Complex", album: topAlbums[4], artist: artist5, durationMs: 258000, trackNumber: 2, averageRating: 4.5, ratingsCount: 5400 },
];

const trendingArtists = [artist1, artist2, artist3, artist4, artist5, artist6];

export default function DiscoverPage() {
  return (
    <div>
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-1">Discover</h1>
        <p className="text-[#a0a0a0]">Explore top albums, trending artists, and popular tracks</p>
      </div>

      {/* Top Albums */}
      <section className="mb-12">
        <h2 className="text-xl font-semibold text-white mb-4">🏆 Top Albums</h2>
        <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-6 gap-4">
          {topAlbums.map((album) => (
            <AlbumCard key={album.id} album={album} />
          ))}
        </div>
      </section>

      {/* Trending Artists */}
      <section className="mb-12">
        <h2 className="text-xl font-semibold text-white mb-4">🔥 Trending Artists</h2>
        <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-6 gap-4">
          {trendingArtists.map((artist, idx) => (
            <div
              key={artist.id}
              className="bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl p-4 flex flex-col items-center gap-3 hover:border-[#1db954] transition-colors cursor-pointer"
            >
              <div
                className="w-16 h-16 rounded-full overflow-hidden bg-[#2a2a2a] flex items-center justify-center"
              >
                <img
                  src={`https://picsum.photos/seed/artist${idx}/200/200`}
                  alt={artist.name}
                  className="w-full h-full object-cover"
                />
              </div>
              <div className="text-center">
                <p className="text-white font-semibold text-sm">{artist.name}</p>
                <p className="text-[#a0a0a0] text-xs">{(artist.followersCount / 1000000).toFixed(1)}M followers</p>
              </div>
            </div>
          ))}
        </div>
      </section>

      {/* Top Tracks */}
      <section>
        <h2 className="text-xl font-semibold text-white mb-4">🎵 Top Tracks</h2>
        <div className="flex flex-col gap-3">
          {topTracks.map((track, idx) => (
            <TrackCard key={track.id} track={track} rank={idx + 1} />
          ))}
        </div>
      </section>
    </div>
  );
}
