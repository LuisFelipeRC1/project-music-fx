# Architecture — MusicXD

MusicXD is a social music platform where users discover, rate, and review albums and tracks, follow other listeners, and see a personalized activity feed.

---

## System Overview

```
┌──────────────────────────────────────────────────────────────┐
│                        Users (Browser)                        │
└──────────────────────────┬───────────────────────────────────┘
                           │ HTTPS
┌──────────────────────────▼───────────────────────────────────┐
│              Next.js 15 (Vercel)                              │
│  App Router · Server Components · TailwindCSS · shadcn/ui    │
└──────────────────────────┬───────────────────────────────────┘
                           │ REST / JSON
┌──────────────────────────▼───────────────────────────────────┐
│         ASP.NET Core 8 Web API (Railway)                      │
│  Clean Architecture · CQRS/MediatR · JWT Auth · Swagger      │
├──────────┬──────────────────────────────┬────────────────────┤
│ PostgreSQL│          Redis               │   Spotify API      │
│ (data)   │  (caching, session)           │   (music catalog)  │
└──────────┴──────────────────────────────┴────────────────────┘
```

---

## Backend Architecture: Clean Architecture

The backend is organized into 4 layers with strict dependency rules — inner layers never depend on outer ones.

```
MusicXD.Domain          ← no external dependencies
MusicXD.Application     ← depends on Domain only
MusicXD.Infrastructure  ← depends on Application + Domain
MusicXD.API             ← depends on all layers (composition root)
```

### Layer Responsibilities

| Layer | Responsibility | Key Files |
|-------|---------------|-----------|
| **Domain** | Business entities, no framework dependencies | `Entities/User.cs`, `Entities/Album.cs` |
| **Application** | Use cases via CQRS, interfaces, DTOs | `Features/`, `Interfaces/`, `DTOs/` |
| **Infrastructure** | EF Core, PostgreSQL, Redis, Spotify, JWT | `Persistence/`, `Services/`, `Caching/` |
| **API** | HTTP controllers, middleware, DI setup | `Controllers/`, `Middleware/`, `Program.cs` |

### Request Flow

```
HTTP Request
  → ASP.NET Core Middleware (auth, exceptions)
  → Controller
  → MediatR.Send(command/query)
  → Handler (Application layer)
  → Repository / Service (via interface)
  → Infrastructure implementation
  → PostgreSQL / Redis / Spotify API
  → Response mapped to DTO
  → HTTP Response
```

---

## Frontend Architecture: Next.js App Router

```
musicxd.web/src/
├── app/                    # Next.js App Router (file-based routing)
│   ├── layout.tsx          # Root layout (providers, fonts)
│   ├── page.tsx            # Home feed (/)
│   ├── album/[id]/         # Album detail (/album/:id)
│   ├── track/[id]/         # Track detail (/track/:id)
│   ├── profile/[id]/       # User profile (/profile/:id)
│   ├── discover/           # Discover/trending (/discover)
│   ├── search/             # Search (/search)
│   ├── login/              # Auth (/login)
│   └── register/           # Auth (/register)
│
├── components/
│   ├── ui/                 # shadcn/ui components (auto-generated)
│   ├── shared/             # Custom reusable components
│   └── features/           # Feature-specific components
│       ├── album/
│       ├── track/
│       ├── user/
│       └── feed/
│
├── lib/
│   └── api.ts              # Type-safe API client
│
└── types/
    └── index.ts            # Shared TypeScript interfaces
```

**Rendering strategy:** Server Components by default. `'use client'` only when needed for interactivity.

---

## Domain Model

```
User ──────── Follow ──────── User
  │
  ├── AlbumReview ──── Album ──── Artist
  │                      └────── Track
  ├── TrackReview ──── Track
  │
  └── ActivityFeed (aggregates user actions)
```

### Entities

| Entity | Key Fields |
|--------|-----------|
| `User` | Id, Username, Email, PasswordHash, SpotifyId, Bio, AvatarUrl |
| `Artist` | Id, SpotifyId, Name, Genres, ImageUrl |
| `Album` | Id, SpotifyId, Title, ArtistId, ReleaseDate, CoverUrl |
| `Track` | Id, SpotifyId, Title, AlbumId, DurationMs, TrackNumber |
| `AlbumReview` | Id, UserId, AlbumId, Rating (1-5), ReviewText, CreatedAt |
| `TrackReview` | Id, UserId, TrackId, Rating (1-5), ReviewText, CreatedAt |
| `Follow` | FollowerId, FollowingId, CreatedAt |
| `ActivityFeed` | Id, UserId, ActivityType, ReferenceId, CreatedAt |

---

## External Integrations

### Spotify API

- **Purpose:** Source of truth for music catalog (artists, albums, tracks)
- **Authentication:** Client Credentials flow (no user auth required for catalog)
- **Sync strategy:** `SpotifySyncJob` background service runs periodically to sync popular content
- **Caching:** Redis caches Spotify responses (TTL: 1 hour) to reduce API calls
- **Rate limiting:** API has a 429 handler with retry logic

### Authentication

- **Strategy:** JWT Bearer tokens (stateless)
- **Access token:** Short-lived (15min default)
- **Refresh token:** Planned (Issue #11)
- **Storage (frontend):** HttpOnly cookies (planned) or localStorage (current)

---

## Infrastructure

### Development (Docker Compose)

```yaml
services:
  postgres:  PostgreSQL 16 Alpine  (port 5432)
  redis:     Redis 7 Alpine        (port 6379)
  api:       .NET 8 API            (port 5000)
  web:       Next.js               (port 3000)
```

Start with: `docker-compose up`

### Production

| Component | Platform |
|-----------|---------|
| Frontend | Vercel |
| Backend API | Railway |
| PostgreSQL | Railway (managed) |
| Redis | Railway (managed) |

---

## Architectural Decision Records (ADRs)

### ADR-001: Clean Architecture for Backend
**Decision:** Use Clean Architecture with Domain → Application → Infrastructure → API layers.
**Rationale:** Enforces separation of concerns, testability, and prevents business logic from leaking into infrastructure code. Domain and Application layers can be tested without a database.

### ADR-002: CQRS with MediatR
**Decision:** Use Command/Query Responsibility Segregation via MediatR pipeline.
**Rationale:** Makes the intent of each operation explicit, enables easy cross-cutting concerns (logging, validation, caching) via MediatR pipeline behaviors, and keeps handlers focused and testable.

### ADR-003: JWT for Authentication (Stateless)
**Decision:** JWT Bearer tokens stored on the client, validated on every request.
**Rationale:** No server-side session state required. Works well with the stateless API deployment on Railway. Trade-off: tokens can't be invalidated before expiry without a blocklist.

### ADR-004: Redis for Caching
**Decision:** Use Redis to cache Spotify API responses and hot data.
**Rationale:** Spotify API has rate limits. Caching reduces external calls and latency for frequently accessed music catalog data.

### ADR-005: Spotify as Music Catalog Source
**Decision:** Fetch and sync music metadata from Spotify API rather than building our own catalog.
**Rationale:** Spotify has comprehensive, high-quality music metadata. Syncing into our own PostgreSQL gives us full query control while relying on Spotify for catalog accuracy.

### ADR-006: Next.js App Router with Server Components
**Decision:** Use Next.js 15 App Router with React Server Components as the default.
**Rationale:** Server Components reduce client-side JavaScript, improve initial load performance, and allow direct data fetching at the component level. `'use client'` boundary is used only where interactivity is required.
