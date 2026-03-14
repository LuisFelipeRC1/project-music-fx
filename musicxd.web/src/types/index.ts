export interface User {
  id: string;
  username: string;
  email: string;
  displayName: string;
  avatarUrl?: string;
  bio?: string;
  followersCount: number;
  followingCount: number;
  reviewsCount: number;
  createdAt: string;
}

export interface Artist {
  id: string;
  name: string;
  imageUrl?: string;
  bio?: string;
  followersCount: number;
}

export interface Album {
  id: string;
  title: string;
  artist: Artist;
  coverUrl?: string;
  releaseYear: number;
  genre?: string;
  tracksCount: number;
  averageRating?: number;
  ratingsCount: number;
}

export interface Track {
  id: string;
  title: string;
  album: Album;
  artist: Artist;
  durationMs: number;
  trackNumber: number;
  averageRating?: number;
  ratingsCount: number;
}

export interface AlbumReview {
  id: string;
  user: User;
  album: Album;
  rating: number;
  content: string;
  createdAt: string;
  likesCount: number;
}

export interface TrackReview {
  id: string;
  user: User;
  track: Track;
  rating: number;
  content: string;
  createdAt: string;
  likesCount: number;
}

export type ActivityType =
  | "reviewed_album"
  | "reviewed_track"
  | "rated_album"
  | "rated_track"
  | "followed_user";

export interface ActivityFeed {
  id: string;
  user: User;
  type: ActivityType;
  album?: Album;
  track?: Track;
  targetUser?: User;
  review?: AlbumReview | TrackReview;
  rating?: number;
  createdAt: string;
}
