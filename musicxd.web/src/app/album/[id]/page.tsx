import ReviewCard from "@/components/ReviewCard";
import StarRating from "@/components/StarRating";
import { Album, AlbumReview, Artist } from "@/types";

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

const mockReviews: AlbumReview[] = [
  {
    id: "r1",
    user: { id: "u1", username: "musiclover", email: "", displayName: "Alex Rivers", followersCount: 320, followingCount: 210, reviewsCount: 87, createdAt: "2022-03-15" },
    album: mockAlbum,
    rating: 5,
    content: "An absolutely stunning album. Kevin Parker at the peak of his craft — every track bleeds into the next like a dream sequence. 'Let It Happen' alone is worth the price of admission.",
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 2).toISOString(),
    likesCount: 42,
  },
  {
    id: "r2",
    user: { id: "u2", username: "vinyl_vibes", email: "", displayName: "Sam Lane", followersCount: 540, followingCount: 180, reviewsCount: 152, createdAt: "2021-07-01" },
    album: mockAlbum,
    rating: 4,
    content: "A polished and introspective record that works best as a whole. Some tracks drag in the middle but the highs are incredibly high.",
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 24 * 3).toISOString(),
    likesCount: 18,
  },
  {
    id: "r3",
    user: { id: "u3", username: "waveguide", email: "", displayName: "Jordan Kai", followersCount: 110, followingCount: 95, reviewsCount: 33, createdAt: "2023-01-20" },
    album: mockAlbum,
    rating: 4.5,
    content: "'Yes I'm Changing' and 'Eventually' hit differently every listen. This album gets better with age.",
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 24 * 10).toISOString(),
    likesCount: 29,
  },
];

const tracklist = [
  "Let It Happen", "Nangs", "The Moment", "Yes I'm Changing", "Eventually",
  "Gossip", "The Less I Know the Better", "Past Life", "Disciples",
  "Cause I'm a Man", "Reality in Motion", "Love/Paranoia", "New Person, Same Old Mistakes",
];

export default function AlbumDetailPage() {
  return (
    <div>
      {/* Album header */}
      <div className="flex flex-col sm:flex-row gap-6 mb-10">
        <div className="w-48 h-48 rounded-xl overflow-hidden bg-[#2a2a2a] shrink-0 self-start">
          {mockAlbum.coverUrl && (
            <img src={mockAlbum.coverUrl} alt={mockAlbum.title} className="w-full h-full object-cover" />
          )}
        </div>

        <div className="flex flex-col justify-end">
          <p className="text-[#a0a0a0] text-xs uppercase tracking-widest mb-1">Album</p>
          <h1 className="text-4xl font-bold text-white mb-1">{mockAlbum.title}</h1>
          <p className="text-[#1db954] font-semibold text-lg mb-1">{mockAlbum.artist.name}</p>
          <p className="text-[#a0a0a0] text-sm mb-3">
            {mockAlbum.releaseYear} · {mockAlbum.genre} · {mockAlbum.tracksCount} tracks
          </p>
          {mockAlbum.averageRating !== undefined && (
            <div className="flex items-center gap-2">
              <StarRating rating={mockAlbum.averageRating} size="md" />
              <span className="text-[#a0a0a0] text-sm">({mockAlbum.ratingsCount.toLocaleString()} ratings)</span>
            </div>
          )}
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        {/* Tracklist */}
        <div className="lg:col-span-1">
          <h2 className="text-lg font-semibold text-white mb-4">Tracklist</h2>
          <div className="bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl overflow-hidden">
            {tracklist.map((title, idx) => (
              <div
                key={idx}
                className="flex items-center gap-3 px-4 py-3 border-b border-[#2a2a2a] last:border-0 hover:bg-[#2a2a2a] transition-colors cursor-pointer group"
              >
                <span className="text-[#a0a0a0] text-xs w-5 text-right shrink-0">{idx + 1}</span>
                <span className="text-white text-sm group-hover:text-[#1db954] transition-colors">{title}</span>
              </div>
            ))}
          </div>
        </div>

        {/* Reviews */}
        <div className="lg:col-span-2">
          <div className="flex items-center justify-between mb-4">
            <h2 className="text-lg font-semibold text-white">Reviews</h2>
            <button className="bg-[#1db954] hover:bg-[#17a349] text-white text-sm font-semibold px-4 py-2 rounded-full transition-colors">
              Write a Review
            </button>
          </div>

          <div className="flex flex-col gap-4">
            {mockReviews.map((review) => (
              <ReviewCard key={review.id} review={review} />
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
