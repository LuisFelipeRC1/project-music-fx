const API_URL = process.env.NEXT_PUBLIC_API_URL ?? "http://localhost:5000";

async function request<T>(path: string, options?: RequestInit): Promise<T> {
  const res = await fetch(`${API_URL}${path}`, {
    headers: { "Content-Type": "application/json", ...options?.headers },
    ...options,
  });
  if (!res.ok) {
    const error = await res.text();
    throw new Error(error || `Request failed: ${res.status}`);
  }
  return res.json() as Promise<T>;
}

export const api = {
  // Auth
  login: (email: string, password: string) =>
    request("/auth/login", { method: "POST", body: JSON.stringify({ email, password }) }),
  register: (username: string, email: string, password: string) =>
    request("/auth/register", { method: "POST", body: JSON.stringify({ username, email, password }) }),

  // Feed
  getFeed: () => request("/feed"),

  // Albums
  getAlbum: (id: string) => request(`/albums/${id}`),
  getAlbumReviews: (id: string) => request(`/albums/${id}/reviews`),
  createAlbumReview: (id: string, rating: number, content: string) =>
    request(`/albums/${id}/reviews`, { method: "POST", body: JSON.stringify({ rating, content }) }),

  // Tracks
  getTrack: (id: string) => request(`/tracks/${id}`),
  getTrackReviews: (id: string) => request(`/tracks/${id}/reviews`),

  // Users
  getUser: (id: string) => request(`/users/${id}`),
  followUser: (id: string) => request(`/users/${id}/follow`, { method: "POST" }),
  unfollowUser: (id: string) => request(`/users/${id}/unfollow`, { method: "POST" }),

  // Discover
  getTopAlbums: () => request("/discover/top-albums"),
  getTrendingArtists: () => request("/discover/trending-artists"),
  getTopTracks: () => request("/discover/top-tracks"),

  // Search
  search: (query: string) => request(`/search?q=${encodeURIComponent(query)}`),
};
