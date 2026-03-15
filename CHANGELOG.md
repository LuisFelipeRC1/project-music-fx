# Changelog — MusicXD

All notable changes to this project will be documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/) and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

### Added
- Full project scaffold: .NET 8 Clean Architecture backend + Next.js 15 frontend
- Complete documentation suite: architecture, git flow, code standards, design system, CI/CD
- GitHub issue templates (bug, feature, task, tech-debt) and PR template
- CI pipeline: backend build/test + frontend lint/type-check/build
- CD pipelines: Vercel (frontend) + Railway (backend)
- Design system tokens (colors, typography, spacing) in `tailwind.config.ts`
- Dependabot configuration for automated dependency updates
- Commitlint enforcement for Conventional Commits
- SECURITY.md with vulnerability reporting process
- LICENSE (MIT)
- `.editorconfig` for consistent formatting
- CODEOWNERS for automatic PR review assignment

---

## How to Update This File

When merging a PR that contains user-facing changes, add an entry under `[Unreleased]`:

```markdown
### Added
- Brief description of new feature

### Changed
- Brief description of changed behavior

### Fixed
- Brief description of bug fix

### Removed
- Brief description of removed functionality

### Security
- Brief description of security fix
```

When cutting a release, move the `[Unreleased]` section content to a new versioned entry:

```markdown
## [1.0.0] - 2024-MM-DD

### Added
...
```

And update the comparison links at the bottom of this file.

---

[Unreleased]: https://github.com/LuisFelipeRC1/project-music-fx/compare/HEAD...HEAD
