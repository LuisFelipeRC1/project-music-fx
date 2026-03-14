"use client";

import { useState } from "react";
import AlbumCard from "@/components/AlbumCard";
import TrackCard from "@/components/TrackCard";
import Link from "next/link";
import { Album, Artist, Track, User } from "@/types";

const mockArtist1: Artist = { id: "a1", name: "Tame Impala", followersCount: 5200000 };
const mockArtist2: Artist = { id: "a2", name: "Radiohead", followersCount: 7800000 };

const mockAlbums: Album[] = [
  { id: "alb1", title: "Currents", artist: mockArtist1, coverUrl: "https://picsum.photos/seed/currents/300/300", releaseYear: 2015, tracksCount: 13, averageRating: 4.6, ratingsCount: 12400 },
  { id: "alb2", title: "OK Computer", artist: mockArtist2, coverUrl: "https://picsum.photos/seed/okcomputer/300/300", releaseYear: 1997, tracksCount: 12, averageRating: 4.8, ratingsCount: 22100 },
];

const mockTracks: Track[] = [
  { id: "t1", title: "Let It Happen", album: mockAlbums[0], artist: mockArtist1, durationMs: 467000, trackNumber: 1, averageRating: 4.8, ratingsCount: 8200 },
  { id: "t2", title: "Paranoid Android", album: mockAlbums[1], artist: mockArtist2, durationMs: 386000, trackNumber: 2, averageRating: 4.9, ratingsCount: 15400 },
];

const mockUsers: User[] = [
  { id: "u1", username: "musiclover", email: "", displayName: "Alex Rivers", followersCount: 320, followingCount: 210, reviewsCount: 87, createdAt: "2022-03-15" },
  { id: "u2", username: "vinyl_vibes", email: "", displayName: "Sam Lane", followersCount: 540, followingCount: 180, reviewsCount: 152, createdAt: "2021-07-01" },
];

const mockArtists: Artist[] = [mockArtist1, mockArtist2];

type Tab = "albums" | "tracks" | "artists" | "users";

export default function SearchPage() {
  const [query, setQuery] = useState("");
  const [activeTab, setActiveTab] = useState<Tab>("albums");
  const hasQuery = query.trim().length > 0;

  const tabs: { key: Tab; label: string }[] = [
    { key: "albums", label: "Albums" },
    { key: "tracks", label: "Tracks" },
    { key: "artists", label: "Artists" },
    { key: "users", label: "Users" },
  ];

  return (
    <div>
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-4">Search</h1>
        <div className="relative">
          <svg className="absolute left-4 top-1/2 -translate-y-1/2 w-5 h-5 text-[#a0a0a0]" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
          </svg>
          <input
            type="text"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            placeholder="Search albums, tracks, artists, users…"
            className="w-full bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl pl-12 pr-4 py-4 text-white placeholder-[#555] text-sm focus:outline-none focus:border-[#1db954] transition-colors"
          />
        </div>
      </div>

      {!hasQuery ? (
        <div className="text-center py-16 text-[#a0a0a0]">
          <svg className="w-16 h-16 mx-auto mb-4 opacity-30" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
          </svg>
          <p className="text-lg font-medium">Start typing to search</p>
          <p className="text-sm mt-1">Find albums, tracks, artists, and users</p>
        </div>
      ) : (
        <>
          {/* Tabs */}
          <div className="flex gap-1 mb-6 bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl p-1 w-fit">
            {tabs.map((tab) => (
              <button
                key={tab.key}
                onClick={() => setActiveTab(tab.key)}
                className={`px-4 py-2 rounded-lg text-sm font-medium transition-colors ${
                  activeTab === tab.key
                    ? "bg-[#1db954] text-white"
                    : "text-[#a0a0a0] hover:text-white"
                }`}
              >
                {tab.label}
              </button>
            ))}
          </div>

          {/* Results */}
          {activeTab === "albums" && (
            <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-5 gap-4">
              {mockAlbums.map((album) => (
                <AlbumCard key={album.id} album={album} />
              ))}
            </div>
          )}

          {activeTab === "tracks" && (
            <div className="flex flex-col gap-3">
              {mockTracks.map((track, idx) => (
                <TrackCard key={track.id} track={track} rank={idx + 1} />
              ))}
            </div>
          )}

          {activeTab === "artists" && (
            <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 gap-4">
              {mockArtists.map((artist, idx) => (
                <div
                  key={artist.id}
                  className="bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl p-4 flex flex-col items-center gap-3 hover:border-[#1db954] transition-colors"
                >
                  <div className="w-20 h-20 rounded-full overflow-hidden bg-[#2a2a2a]">
                    <img src={`https://picsum.photos/seed/artist${idx + 1}/200/200`} alt={artist.name} className="w-full h-full object-cover" />
                  </div>
                  <div className="text-center">
                    <p className="text-white font-semibold text-sm">{artist.name}</p>
                    <p className="text-[#a0a0a0] text-xs">{(artist.followersCount / 1000000).toFixed(1)}M followers</p>
                  </div>
                </div>
              ))}
            </div>
          )}

          {activeTab === "users" && (
            <div className="flex flex-col gap-3">
              {mockUsers.map((user) => (
                <Link href={`/profile/${user.id}`} key={user.id}>
                  <div className="bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl p-4 flex items-center gap-4 hover:border-[#1db954] transition-colors">
                    <div className="w-12 h-12 rounded-full bg-[#1db954] flex items-center justify-center text-white font-bold">
                      {user.displayName[0].toUpperCase()}
                    </div>
                    <div>
                      <p className="text-white font-semibold">{user.displayName}</p>
                      <p className="text-[#a0a0a0] text-sm">@{user.username} · {user.reviewsCount} reviews</p>
                    </div>
                  </div>
                </Link>
              ))}
            </div>
          )}
        </>
      )}
    </div>
  );
}
