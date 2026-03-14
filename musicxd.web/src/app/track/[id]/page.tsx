import ReviewCard from "@/components/ReviewCard";
import StarRating from "@/components/StarRating";
import { Album, Artist, Track, TrackReview } from "@/types";

const mockArtist: Artist = { id: "a1", name: "Tame Impala", followersCount: 5200000 };

const mockAlbum: Album = {
  id: "alb1",
  title: "Currents",
  artist: mockArtist,
  coverUrl: "https://picsum.photos/seed/currents/300/300",
  releaseYear: 2015,
  genre: "Psychedelic Pop",
  tracksCount: 13,
  averageRating: 4.6,
  ratingsCount: 12400,
};

const mockTrack: Track = {
  id: "t1",
  title: "Let It Happen",
  album: mockAlbum,
  artist: mockArtist,
  durationMs: 467000,
  trackNumber: 1,
  averageRating: 4.8,
  ratingsCount: 8200,
};

const mockReviews: TrackReview[] = [
  {
    id: "tr1",
    user: { id: "u1", username: "musiclover", email: "", displayName: "Alex Rivers", followersCount: 320, followingCount: 210, reviewsCount: 87, createdAt: "2022-03-15" },
    track: mockTrack,
    rating: 5,
    content: "Seven and a half minutes of pure euphoria. The way the track builds and then collapses mid-way is unlike anything in modern music. Kevin Parker is a genius.",
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 3).toISOString(),
    likesCount: 67,
  },
  {
    id: "tr2",
    user: { id: "u2", username: "vinyl_vibes", email: "", displayName: "Sam Lane", followersCount: 540, followingCount: 180, reviewsCount: 152, createdAt: "2021-07-01" },
    track: mockTrack,
    rating: 4.5,
    content: "Perfect opener to Currents. Sets the tone for the whole album with those dreamy synths.",
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 24 * 5).toISOString(),
    likesCount: 34,
  },
];

function formatDuration(ms: number): string {
  const totalSeconds = Math.floor(ms / 1000);
  const minutes = Math.floor(totalSeconds / 60);
  const seconds = totalSeconds % 60;
  return `${minutes}:${seconds.toString().padStart(2, "0")}`;
}

export default function TrackDetailPage() {
  return (
    <div>
      {/* Track header */}
      <div className="flex flex-col sm:flex-row gap-6 mb-10">
        <div className="w-40 h-40 rounded-xl overflow-hidden bg-[#2a2a2a] shrink-0 self-start">
          {mockAlbum.coverUrl && (
            <img src={mockAlbum.coverUrl} alt={mockAlbum.title} className="w-full h-full object-cover" />
          )}
        </div>

        <div className="flex flex-col justify-end">
          <p className="text-[#a0a0a0] text-xs uppercase tracking-widest mb-1">Track {mockTrack.trackNumber}</p>
          <h1 className="text-3xl font-bold text-white mb-1">{mockTrack.title}</h1>
          <p className="text-[#1db954] font-semibold text-lg mb-1">{mockTrack.artist.name}</p>
          <p className="text-[#a0a0a0] text-sm mb-1">
            From <a href={`/album/${mockAlbum.id}`} className="text-white hover:text-[#1db954] transition-colors">{mockAlbum.title}</a>
            {" "}· {mockAlbum.releaseYear} · {formatDuration(mockTrack.durationMs)}
          </p>
          {mockTrack.averageRating !== undefined && (
            <div className="flex items-center gap-2 mt-2">
              <StarRating rating={mockTrack.averageRating} size="md" />
              <span className="text-[#a0a0a0] text-sm">({mockTrack.ratingsCount.toLocaleString()} ratings)</span>
            </div>
          )}
        </div>
      </div>

      {/* Reviews */}
      <div className="max-w-2xl">
        <div className="flex items-center justify-between mb-4">
          <h2 className="text-lg font-semibold text-white">Reviews</h2>
          <button className="bg-[#1db954] hover:bg-[#17a349] text-white text-sm font-semibold px-4 py-2 rounded-full transition-colors">
            Rate &amp; Review
          </button>
        </div>

        <div className="flex flex-col gap-4">
          {mockReviews.map((review) => (
            <ReviewCard key={review.id} review={review} />
          ))}
        </div>
      </div>
    </div>
  );
}
