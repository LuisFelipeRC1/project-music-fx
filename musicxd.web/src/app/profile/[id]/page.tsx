import ReviewCard from "@/components/ReviewCard";
import AlbumCard from "@/components/AlbumCard";
import { Album, AlbumReview, Artist, User } from "@/types";
import Link from "next/link";

const mockUser: User = {
  id: "u1",
  username: "musiclover",
  email: "ml@example.com",
  displayName: "Alex Rivers",
  bio: "Music obsessive. Vinyl collector. Finding beauty in sound since 2003.",
  followersCount: 320,
  followingCount: 210,
  reviewsCount: 87,
  createdAt: "2022-03-15",
};

const mockArtist: Artist = { id: "a1", name: "Tame Impala", followersCount: 5200000 };
const mockArtist2: Artist = { id: "a2", name: "Radiohead", followersCount: 7800000 };

const favoriteAlbums: Album[] = [
  { id: "alb1", title: "Currents", artist: mockArtist, coverUrl: "https://picsum.photos/seed/currents/300/300", releaseYear: 2015, tracksCount: 13, averageRating: 4.6, ratingsCount: 12400 },
  { id: "alb2", title: "OK Computer", artist: mockArtist2, coverUrl: "https://picsum.photos/seed/okcomputer/300/300", releaseYear: 1997, tracksCount: 12, averageRating: 4.8, ratingsCount: 22100 },
  { id: "alb3", title: "Blonde", artist: { id: "a3", name: "Frank Ocean", followersCount: 9100000 }, coverUrl: "https://picsum.photos/seed/blonde/300/300", releaseYear: 2016, tracksCount: 17, averageRating: 4.7, ratingsCount: 18300 },
];

const recentReviews: AlbumReview[] = [
  {
    id: "r1",
    user: mockUser,
    album: favoriteAlbums[0],
    rating: 5,
    content: "An absolutely stunning album. Kevin Parker at the peak of his craft — every track bleeds into the next like a dream sequence.",
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 2).toISOString(),
    likesCount: 42,
  },
  {
    id: "r2",
    user: mockUser,
    album: favoriteAlbums[1],
    rating: 5,
    content: "A timeless record. Paranoid Android still sounds like the future somehow.",
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 24 * 7).toISOString(),
    likesCount: 31,
  },
];

export default function ProfilePage() {
  return (
    <div>
      {/* Profile header */}
      <div className="flex flex-col sm:flex-row items-start gap-6 mb-10">
        <div className="w-24 h-24 rounded-full bg-[#1db954] flex items-center justify-center text-white font-bold text-3xl shrink-0">
          {mockUser.displayName[0].toUpperCase()}
        </div>

        <div className="flex-1">
          <div className="flex flex-wrap items-start justify-between gap-4">
            <div>
              <h1 className="text-2xl font-bold text-white">{mockUser.displayName}</h1>
              <p className="text-[#a0a0a0] text-sm">@{mockUser.username}</p>
              {mockUser.bio && (
                <p className="text-[#a0a0a0] text-sm mt-2 max-w-md">{mockUser.bio}</p>
              )}
            </div>
            <button className="bg-[#1db954] hover:bg-[#17a349] text-white font-semibold px-5 py-2 rounded-full transition-colors text-sm">
              Follow
            </button>
          </div>

          {/* Stats */}
          <div className="flex gap-6 mt-4">
            {[
              { label: "Reviews", value: mockUser.reviewsCount },
              { label: "Followers", value: mockUser.followersCount },
              { label: "Following", value: mockUser.followingCount },
            ].map((stat) => (
              <div key={stat.label} className="text-center">
                <p className="text-white font-bold text-xl">{stat.value.toLocaleString()}</p>
                <p className="text-[#a0a0a0] text-xs">{stat.label}</p>
              </div>
            ))}
          </div>
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        {/* Favourite Albums */}
        <div className="lg:col-span-1">
          <h2 className="text-lg font-semibold text-white mb-4">Favourite Albums</h2>
          <div className="grid grid-cols-3 gap-2">
            {favoriteAlbums.map((album) => (
              <AlbumCard key={album.id} album={album} />
            ))}
          </div>

          <div className="mt-6 bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl p-4">
            <p className="text-[#a0a0a0] text-xs uppercase tracking-widest mb-3">Member since</p>
            <p className="text-white text-sm">{new Date(mockUser.createdAt).toLocaleDateString("en-US", { year: "numeric", month: "long" })}</p>
          </div>
        </div>

        {/* Recent Reviews */}
        <div className="lg:col-span-2">
          <h2 className="text-lg font-semibold text-white mb-4">Recent Reviews</h2>
          <div className="flex flex-col gap-4">
            {recentReviews.map((review) => (
              <ReviewCard key={review.id} review={review} showSubject />
            ))}
          </div>
          <div className="mt-4">
            <Link href="#" className="text-[#1db954] hover:underline text-sm font-medium">
              View all reviews →
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
}
