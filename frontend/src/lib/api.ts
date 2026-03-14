const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL ?? "http://localhost:5000/api";

async function request<T>(
  path: string,
  options?: RequestInit
): Promise<T> {
  const res = await fetch(`${API_BASE_URL}${path}`, {
    headers: {
      "Content-Type": "application/json",
      ...(options?.headers ?? {}),
    },
    ...options,
  });

  if (!res.ok) {
    const error = await res.json().catch(() => ({ error: res.statusText }));
    throw new Error(error.error ?? "Request failed");
  }

  return res.json() as Promise<T>;
}

export const api = {
  auth: {
    register: (body: { username: string; email: string; password: string }) =>
      request("/auth/register", { method: "POST", body: JSON.stringify(body) }),
    login: (body: { email: string; password: string }) =>
      request("/auth/login", { method: "POST", body: JSON.stringify(body) }),
    me: (token: string) =>
      request("/auth/me", { headers: { Authorization: `Bearer ${token}` } }),
  },
  users: {
    getProfile: (userId: string, token: string) =>
      request(`/users/${userId}`, {
        headers: { Authorization: `Bearer ${token}` },
      }),
    updateProfile: (body: { bio?: string; avatarUrl?: string }, token: string) =>
      request("/users/profile", {
        method: "PUT",
        body: JSON.stringify(body),
        headers: { Authorization: `Bearer ${token}` },
      }),
    search: (q: string, token: string) =>
      request(`/users/search?q=${encodeURIComponent(q)}`, {
        headers: { Authorization: `Bearer ${token}` },
      }),
  },
  reviews: {
    createAlbumReview: (
      body: { albumId: string; rating: number; reviewText?: string },
      token: string
    ) =>
      request("/reviews/albums", {
        method: "POST",
        body: JSON.stringify(body),
        headers: { Authorization: `Bearer ${token}` },
      }),
    getAlbumReviews: (albumId: string) =>
      request(`/reviews/albums/${albumId}`),
    rateTrack: (body: { trackId: string; rating: number }, token: string) =>
      request("/reviews/tracks", {
        method: "POST",
        body: JSON.stringify(body),
        headers: { Authorization: `Bearer ${token}` },
      }),
  },
  follows: {
    follow: (followingId: string, token: string) =>
      request("/follows", {
        method: "POST",
        body: JSON.stringify({ followingId }),
        headers: { Authorization: `Bearer ${token}` },
      }),
    unfollow: (followingId: string, token: string) =>
      request(`/follows/${followingId}`, {
        method: "DELETE",
        headers: { Authorization: `Bearer ${token}` },
      }),
    getFollowers: (userId: string, token: string) =>
      request(`/follows/followers/${userId}`, {
        headers: { Authorization: `Bearer ${token}` },
      }),
    getFollowing: (userId: string, token: string) =>
      request(`/follows/following/${userId}`, {
        headers: { Authorization: `Bearer ${token}` },
      }),
  },
  feed: {
    getFeed: (token: string) =>
      request("/feed", { headers: { Authorization: `Bearer ${token}` } }),
  },
};
