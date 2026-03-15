# MusicXD

A social music discovery platform — rate songs, review albums, follow friends, and discover music through community activity.

> Inspired by Letterboxd, built for music lovers.

[![CI](https://github.com/LuisFelipeRC1/project-music-fx/actions/workflows/ci.yml/badge.svg)](https://github.com/LuisFelipeRC1/project-music-fx/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

## Quick Start

```bash
# 1. Clone and configure
git clone https://github.com/LuisFelipeRC1/project-music-fx.git
cd project-music-fx
cp .env.example .env
# Edit .env — add your Spotify API credentials and a JWT secret

# 2. Start all services
docker-compose up
```

| Service | URL |
|---------|-----|
| Frontend | http://localhost:3000 |
| Backend API | http://localhost:5000 |
| Swagger UI | http://localhost:5000/swagger |

**Without Docker:**
```bash
# Backend
cd musicxd.api && dotnet run --project MusicXD.API

# Frontend
cd musicxd.web && npm install && npm run dev
```

---

## Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Next.js 15, React 18, TypeScript, Tailwind CSS, shadcn/ui |
| Backend | .NET 8, ASP.NET Core, Clean Architecture, CQRS/MediatR |
| Database | PostgreSQL 16 |
| Cache | Redis 7 |
| Auth | JWT Bearer |
| Music data | Spotify Web API |
| Deploy | Vercel (frontend) + Railway (backend) |

---

## Features

- **Music Reviews** — Rate and review albums and tracks (1–5 stars)
- **Social Feed** — Follow friends and see their listening activity
- **Music Discovery** — Trending charts, friend activity, community picks
- **Spotify Integration** — Sync artists, albums, and tracks from Spotify

---

## Project Structure

```
project-music-fx/
├── musicxd.api/          # .NET 8 backend (Clean Architecture)
│   ├── MusicXD.Domain/       # Entities — no external dependencies
│   ├── MusicXD.Application/  # CQRS use cases, DTOs, interfaces
│   ├── MusicXD.Infrastructure/ # EF Core, Redis, Spotify, JWT
│   └── MusicXD.API/          # Controllers, middleware, DI setup
│
├── musicxd.web/          # Next.js 15 frontend
│   └── src/
│       ├── app/              # App Router pages
│       ├── components/       # UI components (shadcn/ui + custom)
│       ├── lib/api.ts        # Typed API client
│       └── types/            # Shared TypeScript interfaces
│
├── .github/
│   ├── workflows/            # CI, CD frontend, CD backend, commitlint
│   └── ISSUE_TEMPLATE/       # Bug, feature, task, tech-debt templates
│
├── docs/                 # Full project documentation
└── docker-compose.yml    # Local development environment
```

---

## Documentation

| Document | Description |
|----------|-------------|
| [CONTRIBUTING.md](CONTRIBUTING.md) | How to contribute — setup, git flow, PR process |
| [docs/GIT_FLOW.md](docs/GIT_FLOW.md) | Branching strategy, Conventional Commits, release process |
| [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) | System design, ADRs, domain model |
| [docs/CI_CD.md](docs/CI_CD.md) | CI/CD pipelines and deployment guide |
| [docs/standards/BACKEND_STANDARDS.md](docs/standards/BACKEND_STANDARDS.md) | C# naming, CQRS patterns, EF Core rules |
| [docs/standards/FRONTEND_STANDARDS.md](docs/standards/FRONTEND_STANDARDS.md) | TypeScript/React patterns, component structure |
| [docs/standards/CODE_REVIEW.md](docs/standards/CODE_REVIEW.md) | Code review checklist |
| [docs/design-system/OVERVIEW.md](docs/design-system/OVERVIEW.md) | Design system philosophy |
| [docs/design-system/TOKENS.md](docs/design-system/TOKENS.md) | Color palette, typography, spacing |
| [docs/design-system/COMPONENTS.md](docs/design-system/COMPONENTS.md) | UI component catalog |
| [SECURITY.md](SECURITY.md) | Security policy and vulnerability reporting |
| [CHANGELOG.md](CHANGELOG.md) | Version history |

---

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for the full guide.

**Quick reference:**
- Branch: `feature/<issue-id>-<slug>` off `main`
- Commits: [Conventional Commits](docs/GIT_FLOW.md#conventional-commits) enforced on PRs
- PRs: CI must pass, use the PR template

---

## License

[MIT](LICENSE) © MusicXD Contributors
