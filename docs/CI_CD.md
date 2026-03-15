# CI/CD — MusicXD

## Overview

MusicXD has three GitHub Actions pipelines:

| Pipeline | File | Trigger | Purpose |
|----------|------|---------|---------|
| CI | `.github/workflows/ci.yml` | PR to main + push to main | Build, lint, type-check, test |
| CD Frontend | `.github/workflows/cd-frontend.yml` | Push to main (web changes) | Deploy to Vercel |
| CD Backend | `.github/workflows/cd-backend.yml` | Push to main (API changes) | Deploy to Railway |

---

## CI Pipeline

Runs on every pull request to `main` and on every push to `main`.

### Jobs (run in parallel)

**`backend-ci`**
1. Checkout code
2. Setup .NET 8
3. `dotnet restore`
4. `dotnet build --configuration Release`
5. `dotnet test --configuration Release`

**`frontend-ci`**
1. Checkout code
2. Setup Node.js 20 (with npm cache)
3. `npm ci`
4. `npm run lint` (ESLint)
5. `npx tsc --noEmit` (TypeScript type-check)
6. `npm run build` (Next.js production build)

Both jobs must pass (green) before a PR can be merged.

### Testing locally before opening a PR

```bash
# Backend
cd musicxd.api
dotnet build --configuration Release
dotnet test --configuration Release

# Frontend
cd musicxd.web
npm run lint
npx tsc --noEmit
npm run build
```

---

## CD Frontend (Vercel)

**Trigger:** push to `main` that changes files in `musicxd.web/` or the workflow file itself.

### How it works

1. Install Vercel CLI
2. `vercel pull` — sync project settings from Vercel
3. `vercel build --prod` — build the Next.js app
4. `vercel deploy --prebuilt --prod` — deploy to production

### Required GitHub Secrets

Go to GitHub repo → Settings → Secrets and variables → Actions:

| Secret | How to get it |
|--------|--------------|
| `VERCEL_TOKEN` | Vercel dashboard → Account Settings → Tokens → Create |
| `VERCEL_ORG_ID` | Run `vercel whoami` locally after `vercel login`, or find in `.vercel/project.json` |
| `VERCEL_PROJECT_ID` | Link project locally with `vercel link`, then check `.vercel/project.json` |

### Setup steps

```bash
# 1. Install Vercel CLI
npm install -g vercel

# 2. Login and link project
cd musicxd.web
vercel login
vercel link    # Creates .vercel/project.json with org and project IDs

# 3. Get IDs for GitHub Secrets
cat .vercel/project.json
# { "orgId": "team_xxxx", "projectId": "prj_xxxx" }
```

---

## CD Backend (Railway)

**Trigger:** push to `main` that changes files in `musicxd.api/` or the workflow file itself.

### Pre-requisite: Link Railway project locally

Before the CD workflow will work, you must link the Railway project:

```bash
# 1. Install Railway CLI
npm install -g @railway/cli

# 2. Login
railway login

# 3. Link project from musicxd.api directory
cd musicxd.api
railway link    # Select your project and service interactively

# 4. Add .railway/ to .gitignore (link files stay local)
echo ".railway/" >> ../.gitignore
```

### Required GitHub Secrets

| Secret | How to get it |
|--------|--------------|
| `RAILWAY_TOKEN` | Railway dashboard → Account Settings → Tokens → New Token |
| `RAILWAY_SERVICE_NAME` | The exact service name in Railway (e.g., `musicxd-api`) |

### How it works

1. Install Railway CLI
2. Authenticate via `RAILWAY_TOKEN` environment variable
3. `railway up --service <SERVICE_NAME>` — deploy the latest code

---

## Environments

The CD pipelines use the GitHub Environment `production` for additional protection (optional: set required reviewers in GitHub → Settings → Environments).

---

## Troubleshooting

### CI fails: `dotnet test` errors

```bash
# Run locally to see full output
cd musicxd.api
dotnet test --configuration Release --verbosity detailed
```

### CI fails: `npm run build` errors

```bash
# Most common: TypeScript errors
cd musicxd.web
npx tsc --noEmit   # See all type errors

# Or Next.js build errors
npm run build
```

### CD Frontend fails: Vercel auth error

- Verify `VERCEL_TOKEN`, `VERCEL_ORG_ID`, and `VERCEL_PROJECT_ID` are all set in GitHub Secrets
- Token must have sufficient scope (not expired)

### CD Backend fails: Railway auth error

- Verify `RAILWAY_TOKEN` is set and not expired
- Verify `RAILWAY_SERVICE_NAME` exactly matches the service name in Railway (case-sensitive)

### CD Backend fails: `railway up` cannot find project

- The project must be linked. Run `railway link` locally in `musicxd.api/`
- Or pass `--project` and `--service` flags with explicit IDs from Railway
