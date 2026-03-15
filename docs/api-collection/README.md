# MusicXD API Collection (Bruno)

This directory contains the [Bruno](https://www.usebruno.com/) API collection for the MusicXD API.

## Setup

1. Install Bruno: https://www.usebruno.com/downloads
2. Open Bruno → "Open Collection" → select `docs/api-collection/musicxd-api/`
3. Select the **local** environment
4. Start the API: `docker-compose up` or `dotnet run --project musicxd.api/MusicXD.API`

## Authentication Flow

1. Run **Auth → Register** to create a test user
2. Run **Auth → Login** — the token is automatically saved to the `token` environment variable
3. All other requests use that token automatically via `Bearer {{token}}`

## Requests

| Folder | Request | Description |
|--------|---------|-------------|
| Auth | Register | Create new user account |
| Auth | Login | Authenticate and get JWT |
| Albums | Get Album Reviews | List reviews for an album |
| Albums | Create Album Review | Post a new album review |
| Tracks | Create Track Review | Post a new track review |
| Users | Get User Profile | Fetch user profile |
| Users | Follow User | Follow another user |
| Spotify | Search Spotify | Search artists/albums/tracks |

## Why Bruno?

Bruno is open-source, stores collections as plain files (`.bru`), and works well in git repositories — no API key or cloud account required.
