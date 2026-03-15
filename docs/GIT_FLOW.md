# Git Flow — MusicXD

## Strategy: GitHub Flow

All development happens in short-lived feature branches that merge into `main`. The `main` branch is always deployable.

```
main  (always deployable — protected)
 ├── feature/12-user-authentication
 ├── feature/18-album-review-ui
 ├── fix/22-spotify-rate-limit
 └── chore/30-update-dependencies
```

---

## Branch Naming

```
feature/<issue-id>-<slug>   → feature/12-user-authentication
fix/<issue-id>-<slug>       → fix/22-spotify-rate-limit-error
chore/<issue-id>-<slug>     → chore/30-update-dependencies
docs/<issue-id>-<slug>      → docs/5-api-documentation
```

Rules:
- Always start from latest `main`
- Use the GitHub Issue number as `<issue-id>`
- Slug is lowercase, hyphen-separated, max ~4 words
- Never commit directly to `main`

---

## Conventional Commits

Format: `<type>(<scope>): <description>`

```
feat(auth): add JWT refresh token rotation
fix(spotify): handle rate limit 429 responses
docs(contributing): add PR checklist
chore(deps): bump next from 15.5.12 to 15.6.0
refactor(albums): extract review validation to FluentValidation
test(auth): add unit tests for login command handler
perf(feed): cache activity feed with Redis
ci(workflows): add frontend type-check step
```

### Types

| Type | Purpose |
|------|---------|
| `feat` | New feature for the user |
| `fix` | Bug fix for the user |
| `docs` | Documentation only changes |
| `style` | Formatting, no logic change (whitespace, semicolons) |
| `refactor` | Code restructure with no feature change or fix |
| `test` | Adding missing tests or correcting existing tests |
| `chore` | Build process, dependency updates, tooling |
| `perf` | Performance improvement |
| `ci` | Changes to CI/CD pipeline configuration |

### Scopes

| Scope | Area |
|-------|------|
| `auth` | Authentication & authorization |
| `albums` | Album features |
| `tracks` | Track features |
| `users` | User profiles & social features |
| `feed` | Activity feed |
| `spotify` | Spotify API integration |
| `api` | Backend API layer |
| `web` | Frontend app |
| `infra` | Infrastructure (Docker, database) |
| `deps` | Dependency updates |

### Body & Footer (optional)

```
feat(auth): implement refresh token rotation

Previous implementation stored tokens without rotation, creating
security risk if a token was intercepted. This change invalidates
the previous refresh token on each use.

BREAKING CHANGE: clients must handle 401 on expired refresh tokens
Closes #11
```

---

## Branch Protection Rules (main)

Configure in GitHub → Settings → Branches → Add rule for `main`:

| Setting | Value |
|---------|-------|
| Require a pull request before merging | ✅ |
| Require approvals | 1 |
| Require status checks to pass | ✅ |
| Status checks required | `backend-ci`, `frontend-ci` |
| Require branches to be up to date | ✅ |
| Do not allow bypassing the above settings | ✅ |
| Allow force pushes | ❌ |
| Allow deletions | ❌ |
| Automatically delete head branches | ✅ |

---

## Day-to-Day Workflow

```bash
# 1. Start from latest main
git checkout main
git pull origin main

# 2. Create feature branch
git checkout -b feature/42-album-reviews

# 3. Work and commit
git add <files>
git commit -m "feat(albums): add album review creation endpoint"

# 4. Keep branch up to date (if main has moved)
git fetch origin
git rebase origin/main

# 5. Push and open PR
git push -u origin feature/42-album-reviews
# → open PR on GitHub using the PR template

# 6. After merge, clean up locally
git checkout main
git pull origin main
git branch -d feature/42-album-reviews
```

---

## Release Process

Releases follow [Semantic Versioning](https://semver.org/): `MAJOR.MINOR.PATCH`

| Type | When | Example |
|------|------|---------|
| `PATCH` | Bug fixes | `v1.0.1` |
| `MINOR` | New features, backwards-compatible | `v1.1.0` |
| `MAJOR` | Breaking changes | `v2.0.0` |

### Creating a Release

```bash
# Tag main after milestone is complete
git checkout main
git pull origin main
git tag -a v1.0.0 -m "Release v1.0.0 — initial launch"
git push origin v1.0.0
```

Create a GitHub Release from the tag with a changelog.

---

## Hotfix Process

For critical production bugs that can't wait for the normal flow:

```bash
# 1. Branch from main
git checkout main && git pull
git checkout -b fix/99-critical-auth-bypass

# 2. Fix, commit, and push
git commit -m "fix(auth): prevent JWT bypass via null algorithm"
git push -u origin fix/99-critical-auth-bypass

# 3. Open PR → fast-track review → merge
# 4. Tag a patch release
git tag -a v1.0.1 -m "Hotfix v1.0.1 — auth bypass fix"
git push origin v1.0.1
```
