# Contributing to MusicXD

Welcome to MusicXD — a social platform for music lovers. This guide covers everything you need to start contributing.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Local Setup](#local-setup)
- [Git Flow](#git-flow)
- [Commit Standards](#commit-standards)
- [Pull Request Process](#pull-request-process)
- [Further Reading](#further-reading)

---

## Prerequisites

| Tool | Version | Install |
|------|---------|---------|
| Node.js | 20+ | [nodejs.org](https://nodejs.org) |
| .NET SDK | 8.0 | [dotnet.microsoft.com](https://dotnet.microsoft.com/download) |
| Docker | Latest | [docker.com](https://docker.com) |
| Git | Latest | [git-scm.com](https://git-scm.com) |

---

## Local Setup

```bash
# 1. Clone the repository
git clone https://github.com/<org>/project-music-fx.git
cd project-music-fx

# 2. Copy environment variables
cp .env.example .env
# Edit .env with your Spotify API credentials and JWT secret

# 3. Start all services (PostgreSQL, Redis, API, Web)
docker-compose up

# OR run services individually:
# Backend
cd musicxd.api && dotnet run --project MusicXD.API

# Frontend
cd musicxd.web && npm install && npm run dev
```

The app will be available at:
- Frontend: http://localhost:3000
- Backend API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

---

## Git Flow

We use **GitHub Flow**: all development happens in feature branches that merge into `main`.

### Branch Naming

```
feature/<issue-id>-<short-slug>   → feature/12-user-authentication
fix/<issue-id>-<short-slug>       → fix/22-spotify-rate-limit
chore/<issue-id>-<short-slug>     → chore/30-update-dependencies
docs/<issue-id>-<short-slug>      → docs/5-api-documentation
```

Always create a branch from the latest `main`:

```bash
git checkout main && git pull
git checkout -b feature/42-album-reviews
```

See [docs/GIT_FLOW.md](docs/GIT_FLOW.md) for the complete guide including branch protection rules and release process.

---

## Commit Standards

We follow [Conventional Commits](https://www.conventionalcommits.org/):

```
<type>(<scope>): <description>

[optional body]

[optional footer: Closes #123]
```

### Types

| Type | When to use |
|------|-------------|
| `feat` | New feature |
| `fix` | Bug fix |
| `docs` | Documentation only |
| `style` | Formatting, no logic change |
| `refactor` | Code restructure, no feature/fix |
| `test` | Adding or updating tests |
| `chore` | Build, deps, tooling |
| `perf` | Performance improvement |
| `ci` | CI/CD pipeline changes |

### Scopes

`auth` · `albums` · `tracks` · `users` · `feed` · `spotify` · `api` · `web` · `infra` · `deps`

### Examples

```
feat(auth): add JWT refresh token rotation
fix(spotify): handle rate limit 429 responses gracefully
docs(contributing): add PR checklist
chore(deps): bump next from 15.5.12 to 15.6.0
test(auth): add unit tests for login command handler
```

---

## Pull Request Process

1. **Create a branch** from `main` following the naming convention above
2. **Make your changes** following the code standards in [docs/standards/](docs/standards/)
3. **Commit** using Conventional Commits
4. **Open a PR** using the [PR template](.github/PULL_REQUEST_TEMPLATE.md)
5. **Ensure CI is green** — all checks must pass before merging
6. **Request review** if working with a collaborator
7. **Merge** using "Squash and merge" or "Merge commit" (no rebase)
8. **Delete your branch** after merging (done automatically)

### PR Checklist

Before opening a PR, verify:
- [ ] Conventional Commits used
- [ ] No `console.log`, `debugger`, or commented-out code
- [ ] Tests added/updated for new functionality
- [ ] EF Core migration included if schema changed
- [ ] No hardcoded secrets or credentials
- [ ] `npm run lint` passes (frontend)
- [ ] `dotnet build` passes (backend)

---

## Further Reading

| Document | Description |
|----------|-------------|
| [docs/GIT_FLOW.md](docs/GIT_FLOW.md) | Branch strategy, release process, hotfixes |
| [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) | System design and architectural decisions |
| [docs/standards/BACKEND_STANDARDS.md](docs/standards/BACKEND_STANDARDS.md) | C# / .NET naming and patterns |
| [docs/standards/FRONTEND_STANDARDS.md](docs/standards/FRONTEND_STANDARDS.md) | TypeScript / React patterns |
| [docs/standards/CODE_REVIEW.md](docs/standards/CODE_REVIEW.md) | Code review checklist |
| [docs/design-system/TOKENS.md](docs/design-system/TOKENS.md) | Colors, typography, spacing |
| [docs/design-system/COMPONENTS.md](docs/design-system/COMPONENTS.md) | UI component catalog |
| [docs/CI_CD.md](docs/CI_CD.md) | CI/CD pipelines and deployment |
