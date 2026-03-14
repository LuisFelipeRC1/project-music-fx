import ActivityCard from "@/components/ActivityCard";
import { ActivityFeed, Album, User, Artist } from "@/types";

const mockArtist: Artist = { id: "a1", name: "Tame Impala", followersCount: 5200000 };
const mockArtist2: Artist = { id: "a2", name: "Radiohead", followersCount: 7800000 };

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

const mockAlbum2: Album = {
  id: "alb2",
  title: "OK Computer",
  artist: mockArtist2,
  coverUrl: "https://picsum.photos/seed/okcomputer/300/300",
  releaseYear: 1997,
  genre: "Alternative Rock",
  tracksCount: 12,
  averageRating: 4.8,
  ratingsCount: 22100,
};

const mockUser1: User = {
  id: "u1",
  username: "musiclover",
  email: "ml@example.com",
  displayName: "Alex Rivers",
  followersCount: 320,
  followingCount: 210,
  reviewsCount: 87,
  createdAt: "2022-03-15",
};

const mockUser2: User = {
  id: "u2",
  username: "vinyl_vibes",
  email: "vv@example.com",
  displayName: "Sam Lane",
  followersCount: 540,
  followingCount: 180,
  reviewsCount: 152,
  createdAt: "2021-07-01",
};

const mockUser3: User = {
  id: "u3",
  username: "waveguide",
  email: "wg@example.com",
  displayName: "Jordan Kai",
  followersCount: 110,
  followingCount: 95,
  reviewsCount: 33,
  createdAt: "2023-01-20",
};

const mockFeed: ActivityFeed[] = [
  {
    id: "act1",
    user: mockUser1,
    type: "reviewed_album",
    album: mockAlbum,
    rating: 5,
    review: {
      id: "r1",
      user: mockUser1,
      album: mockAlbum,
      rating: 5,
      content:
        "An absolutely stunning album. Kevin Parker at the peak of his craft — every track bleeds into the next like a dream sequence.",
      createdAt: new Date(Date.now() - 1000 * 60 * 35).toISOString(),
      likesCount: 42,
    },
    createdAt: new Date(Date.now() - 1000 * 60 * 35).toISOString(),
  },
  {
    id: "act2",
    user: mockUser2,
    type: "rated_album",
    album: mockAlbum2,
    rating: 4.5,
    createdAt: new Date(Date.now() - 1000 * 60 * 120).toISOString(),
  },
  {
    id: "act3",
    user: mockUser3,
    type: "followed_user",
    targetUser: mockUser1,
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 5).toISOString(),
  },
  {
    id: "act4",
    user: mockUser2,
    type: "reviewed_album",
    album: mockAlbum2,
    rating: 4.5,
    review: {
      id: "r2",
      user: mockUser2,
      album: mockAlbum2,
      rating: 4.5,
      content:
        "A masterpiece that defined a generation. Thom Yorke's vocals paired with those paranoid guitar riffs create something genuinely unlike anything else.",
      createdAt: new Date(Date.now() - 1000 * 60 * 60 * 8).toISOString(),
      likesCount: 89,
    },
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 8).toISOString(),
  },
];

export default function HomePage() {
  return (
    <div className="max-w-2xl mx-auto">
      <div className="mb-8 text-center">
        <h1 className="text-4xl font-bold text-white mb-2">
          Your <span className="text-[#1db954]">Music</span> Feed
        </h1>
        <p className="text-[#a0a0a0]">See what your friends are listening to and reviewing</p>
      </div>

      <div className="flex flex-col gap-4">
        {mockFeed.map((activity) => (
          <ActivityCard key={activity.id} activity={activity} />
        ))}
      </div>

      <div className="mt-10 p-6 bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl text-center">
        <h2 className="text-lg font-semibold text-white mb-2">Join MusicXD</h2>
        <p className="text-[#a0a0a0] text-sm mb-4">
          Track albums you&apos;ve heard, discover new music, and share reviews with friends.
        </p>
        <div className="flex gap-3 justify-center">
          <a
            href="/register"
            className="bg-[#1db954] hover:bg-[#17a349] text-white font-semibold px-6 py-2 rounded-full transition-colors text-sm"
          >
            Get started
          </a>
          <a
            href="/discover"
            className="border border-[#2a2a2a] hover:border-[#1db954] text-white font-semibold px-6 py-2 rounded-full transition-colors text-sm"
          >
            Explore
          </a>
        </div>
      </div>
    </div>
  );
}
