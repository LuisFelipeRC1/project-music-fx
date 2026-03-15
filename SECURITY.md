# Security Policy — MusicXD

## Supported Versions

| Version | Supported |
|---------|-----------|
| `main` branch | ✅ Active |
| Older releases | ❌ Not supported |

## Reporting a Vulnerability

**Do not open a public GitHub Issue for security vulnerabilities.**

If you discover a security vulnerability in MusicXD, please report it responsibly:

1. **Email:** Send a detailed report to the repository owner via GitHub's private vulnerability reporting:
   GitHub repo → Security → Report a vulnerability

2. **Include in your report:**
   - Description of the vulnerability
   - Steps to reproduce
   - Potential impact (what an attacker could achieve)
   - Affected component (backend API, frontend, authentication, etc.)
   - Suggested fix (optional but appreciated)

3. **Response time:** You will receive an acknowledgment within 48 hours and a resolution timeline within 7 days.

## Security Considerations

### Authentication
- JWT tokens are stateless. Treat them as credentials — never log or expose them.
- Refresh tokens (when implemented) must be rotated on every use.
- Passwords are hashed using BCrypt — never stored or logged in plain text.

### Secrets Management
- All secrets (`JWT_SECRET`, `SPOTIFY_CLIENT_SECRET`, database credentials) must be stored in environment variables — never hardcoded.
- The `.env` file must never be committed. It is listed in `.gitignore`.
- GitHub Secrets are used for CI/CD pipelines.

### Known Security Configurations
- CORS is configured to allow only the frontend origin in production.
- Rate limiting is applied to authentication and Spotify proxy endpoints.
- All user inputs are validated server-side via FluentValidation.
- SQL injection is prevented by EF Core's parameterized queries.

## Dependency Security

Dependencies are monitored automatically via **Dependabot** (`.github/dependabot.yml`). Security patches for npm and NuGet packages are automatically proposed as PRs weekly.

To manually audit:
```bash
# Frontend
cd musicxd.web && npm audit

# Backend
cd musicxd.api && dotnet list package --vulnerable
```
