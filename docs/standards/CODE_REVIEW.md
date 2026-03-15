# Code Review Checklist — MusicXD

Use this checklist when reviewing pull requests. A PR should not be approved if any critical item fails.

---

## General (All PRs)

- [ ] The PR description explains **why** the change was made (not just what)
- [ ] The branch follows naming convention: `feature/<id>-<slug>`
- [ ] Commits follow Conventional Commits format
- [ ] No merge commits in the branch history (rebase if needed)
- [ ] CI is green (all checks pass)
- [ ] No hardcoded secrets, credentials, API keys, or connection strings
- [ ] No `console.log`, `debugger`, `TODO` comments, or commented-out code left behind
- [ ] The change is focused — does not include unrelated refactoring

---

## Backend (.NET / C#)

- [ ] Follows naming conventions from [BACKEND_STANDARDS.md](BACKEND_STANDARDS.md)
- [ ] No business logic in controllers — delegated to MediatR handlers
- [ ] No external dependencies in Domain layer
- [ ] New entities have `IEntityTypeConfiguration<T>` class in `Configurations/`
- [ ] EF Core migration included if entity schema changed
- [ ] All async operations use `await`, no `.Result` or `.Wait()`
- [ ] `CancellationToken` passed through all async call chains
- [ ] FluentValidation validator present for new commands
- [ ] Read-only queries use `AsNoTracking()`
- [ ] Tests added/updated for new handlers (xUnit, AAA pattern)
- [ ] No sensitive data returned in DTOs (passwords, internal IDs unnecessarily, etc.)

---

## Frontend (TypeScript / React / Next.js)

- [ ] Follows naming conventions from [FRONTEND_STANDARDS.md](FRONTEND_STANDARDS.md)
- [ ] Component props typed with explicit `interface`
- [ ] No `any` type used
- [ ] `'use client'` only present when truly needed (state, events, browser APIs)
- [ ] No hardcoded hex colors — using Tailwind design tokens (`text-brand`, `bg-surface`, etc.)
- [ ] shadcn/ui components in `components/ui/` not directly modified
- [ ] API calls go through `lib/api.ts`, not raw `fetch` inline in components
- [ ] Loading and error states handled in interactive components
- [ ] No prop drilling more than 2 levels deep
- [ ] `npm run lint` passes
- [ ] TypeScript strict mode: no type errors (`npx tsc --noEmit`)

---

## Security

- [ ] No sensitive data logged (passwords, tokens, PII)
- [ ] User inputs validated on the backend (FluentValidation)
- [ ] Authorization attributes present on protected endpoints (`[Authorize]`)
- [ ] No SQL injection risk (EF Core parameterization used, no raw SQL)
- [ ] CORS policy not overly permissive

---

## Performance

- [ ] Read queries use `AsNoTracking()` in EF Core
- [ ] No N+1 query patterns — eager loading (`Include`) used appropriately
- [ ] Expensive computations not triggered on every render (React `useMemo`/`useCallback` where needed)
- [ ] Images use Next.js `<Image>` component, not raw `<img>`

---

## Severity Guide

| Severity | Action |
|----------|--------|
| **Blocker** | Must be fixed before merge (security issue, broken functionality, missing migration) |
| **Required** | Should be fixed before merge (naming violation, missing tests for new feature) |
| **Suggestion** | Nice to have — can be addressed in a follow-up issue |
| **Nit** | Minor style preference — author's discretion |

Always specify severity when leaving review comments.
