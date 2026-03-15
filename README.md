# 🎧 MusicXD

MusicXD is a **social music discovery platform** inspired by Letterboxd, focused on helping users discover music through community activity.

Users can rate songs, review albums, follow friends, and explore trending music across the platform.

The goal of MusicXD is to combine **music discovery + social interaction** in a single product.

---

# ✨ Features

### 🎵 Music Reviews
Users can:

- Rate songs
- Review albums
- Share opinions about music

---

### 👥 Social Network

MusicXD allows users to:

- Follow friends
- View friends' listening activity
- Like and comment on reviews
- Discover music through community interaction

---

### 📊 Music Leaderboards

Users can explore:

- Top Albums of the Month
- Top Songs of the Week
- Trending Artists
- Friends' favorite tracks

---

### 🔗 Spotify Integration

MusicXD integrates with the Spotify Web API to:

- Sync user listening history
- Show top artists
- Display most played tracks
- Track recently played songs

---

### 🔎 Music Discovery

Users can discover new music through:

- Friend activity
- Community reviews
- Trending charts
- Personalized recommendations

---

# 🏗 Architecture

MusicXD follows **Clean Architecture principles** and is designed to scale as the platform grows.

```
project-music-fx/
├── musicxd.api/              # .NET 8 Backend (Clean Architecture)
│   ├── MusicXD.Domain/       # Entities, no dependencies
│   │   └── Entities/         # User, Artist, Album, Track, AlbumReview, TrackReview, Follow, ActivityFeed
│   ├── MusicXD.Application/  # Business logic, interfaces, DTOs
│   │   ├── Features/         # CQRS commands & queries (MediatR)
│   │   ├── DTOs/             # Data transfer objects
│   │   └── Interfaces/       # IApplicationDbContext, ISpotifyService, IJwtTokenService
│   ├── MusicXD.Infrastructure/ # EF Core, PostgreSQL, Redis, Spotify, JWT
│   │   ├── Persistence/      # ApplicationDbContext + entity configurations
│   │   ├── Services/         # SpotifyService, JwtTokenService
│   │   ├── Caching/          # RedisCacheService
│   │   └── Jobs/             # SpotifySyncJob (IHostedService)
│   └── MusicXD.API/          # ASP.NET Core Web API
│       ├── Controllers/      # Auth, AlbumReviews, TrackReviews, Users, Spotify
│       └── Middleware/       # ExceptionHandlingMiddleware
│
├── musicxd.web/              # Next.js 14 Frontend (TypeScript + TailwindCSS)
│   └── src/
│       ├── app/              # App Router pages
│       │   ├── page.tsx      # Home feed
│       │   ├── login/        # Login page
│       │   ├── register/     # Register page
│       │   ├── album/[id]/   # Album detail + reviews
│       │   ├── track/[id]/   # Track detail + ratings
│       │   ├── profile/[id]/ # User profile
│       │   ├── discover/     # Trending charts
│       │   └── search/       # Search
│       ├── components/       # Navbar, ActivityCard, ReviewCard, StarRating, AlbumCard, TrackCard
│       ├── lib/api.ts        # Fetch-based API client
│       └── types/index.ts    # TypeScript interfaces
│
├── docker-compose.yml        # PostgreSQL + Redis + API + Web
├── .env.example              # Environment variables template
└── .github/workflows/ci.yml  # GitHub Actions CI
```

---

# 🚀 Getting Started

### Prerequisites
- [Docker](https://www.docker.com/) & Docker Compose
- OR: [.NET 8 SDK](https://dotnet.microsoft.com/download) + [Node.js 20+](https://nodejs.org/)

### Run with Docker Compose

```bash
cp .env.example .env
# Edit .env with your Spotify API credentials and JWT secret
docker-compose up
```

- Frontend: http://localhost:3000
- Backend API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

### Run locally (without Docker)

**Backend:**
```bash
cd musicxd.api
dotnet restore
dotnet run --project MusicXD.API
```

**Frontend:**
```bash
cd musicxd.web
npm install
npm run dev
```

---

