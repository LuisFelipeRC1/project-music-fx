# Frontend Code Standards — MusicXD (TypeScript / Next.js / React)

## Naming Conventions

| Element | Convention | Example |
|---------|-----------|---------|
| Components | PascalCase | `AlbumCard.tsx`, `StarRating.tsx` |
| Hooks | `use` prefix + camelCase | `useAlbumReviews.ts`, `useAuth.ts` |
| Utility functions | camelCase | `formatDate.ts`, `truncateText.ts` |
| Types & Interfaces | PascalCase | `AlbumReview`, `UserProfile` |
| Prop types | `<Component>Props` | `AlbumCardProps`, `StarRatingProps` |
| Constants | `UPPER_SNAKE_CASE` | `MAX_REVIEW_LENGTH`, `API_BASE_URL` |
| Event handlers | `handle<Event>` | `handleSubmit`, `handleRatingChange` |
| CSS class helpers | camelCase | (use Tailwind directly) |

---

## Project Structure

```
musicxd.web/src/
├── app/                        # Next.js App Router (routing)
│   ├── layout.tsx              # Root layout with providers
│   ├── page.tsx                # Home feed (/)
│   ├── album/[id]/page.tsx     # Album detail
│   ├── track/[id]/page.tsx     # Track detail
│   ├── profile/[id]/page.tsx   # User profile
│   ├── discover/page.tsx       # Trending
│   ├── search/page.tsx         # Search
│   ├── login/page.tsx          # Login
│   └── register/page.tsx       # Register
│
├── components/
│   ├── ui/                     # shadcn/ui components — DO NOT edit directly
│   ├── shared/                 # Custom reusable components
│   │   ├── Navbar.tsx
│   │   └── StarRating.tsx
│   └── features/               # Feature-specific components
│       ├── album/
│       │   ├── AlbumCard.tsx
│       │   └── AlbumReviewList.tsx
│       ├── track/
│       │   └── TrackCard.tsx
│       ├── user/
│       │   └── UserAvatar.tsx
│       └── feed/
│           └── ActivityCard.tsx
│
├── hooks/                      # Custom React hooks
│   ├── useAuth.ts
│   └── useAlbumReviews.ts
│
├── lib/
│   ├── api.ts                  # Type-safe API client
│   └── utils.ts                # shadcn/ui utilities (cn helper)
│
└── types/
    └── index.ts                # Shared TypeScript interfaces
```

---

## Component Patterns

### Server Components (default)

Use Server Components for any component that:
- Only renders data (no user interaction)
- Fetches data directly from the API
- Does not use browser APIs or React hooks

```tsx
// app/album/[id]/page.tsx — Server Component (no 'use client')
import { AlbumReviewList } from '@/components/features/album/AlbumReviewList'

export default async function AlbumPage({ params }: { params: { id: string } }) {
  const album = await fetchAlbum(params.id)

  return (
    <main>
      <h1 className="text-2xl font-bold text-text">{album.title}</h1>
      <AlbumReviewList albumId={params.id} />
    </main>
  )
}
```

### Client Components

Use `'use client'` only when the component needs:
- `useState`, `useEffect`, or other React hooks
- Browser event listeners
- Browser APIs (`window`, `localStorage`, etc.)

```tsx
'use client'

import { useState } from 'react'
import { Button } from '@/components/ui/button'

interface StarRatingProps {
  initialRating?: number
  onRatingChange: (rating: number) => void
}

export function StarRating({ initialRating = 0, onRatingChange }: StarRatingProps) {
  const [rating, setRating] = useState(initialRating)

  const handleSelect = (value: number) => {
    setRating(value)
    onRatingChange(value)
  }

  return (
    <div className="flex gap-1" role="radiogroup" aria-label="Rating">
      {[1, 2, 3, 4, 5].map((value) => (
        <button
          key={value}
          onClick={() => handleSelect(value)}
          aria-label={`${value} star${value !== 1 ? 's' : ''}`}
          className={value <= rating ? 'text-brand' : 'text-text-muted'}
        >
          ★
        </button>
      ))}
    </div>
  )
}
```

---

## TypeScript Rules

- **Always** type component props with an explicit interface
- **Never** use `any` — use `unknown` if type is truly unknown
- **Enable** strict mode in `tsconfig.json` (already configured)
- **Prefer** `interface` for object shapes, `type` for unions/intersections
- **Export** types/interfaces from `types/index.ts` when shared across components

```typescript
// types/index.ts — shared types
export interface AlbumReview {
  id: string
  userId: string
  albumId: string
  rating: number
  reviewText: string
  createdAt: string
  user: UserProfile
}

export interface UserProfile {
  id: string
  username: string
  avatarUrl: string | null
}
```

---

## API Client

All API calls go through `lib/api.ts`. Functions are typed by resource:

```typescript
// lib/api.ts
const API_BASE = process.env.NEXT_PUBLIC_API_URL ?? 'http://localhost:5000'

async function request<T>(path: string, options?: RequestInit): Promise<T> {
  const res = await fetch(`${API_BASE}${path}`, {
    headers: { 'Content-Type': 'application/json' },
    ...options,
  })

  if (!res.ok) {
    throw new Error(`API error: ${res.status} ${res.statusText}`)
  }

  return res.json() as Promise<T>
}

export const albumsApi = {
  getReviews: (albumId: string) =>
    request<AlbumReview[]>(`/api/albumreviews?albumId=${albumId}`),
  createReview: (data: CreateAlbumReviewDto) =>
    request<AlbumReview>('/api/albumreviews', {
      method: 'POST',
      body: JSON.stringify(data),
    }),
}
```

---

## Styling with Tailwind CSS

- Use design tokens defined in `tailwind.config.ts` (e.g., `bg-surface`, `text-brand`)
- Never hardcode hex colors in className — always use tokens
- Use `cn()` from `lib/utils.ts` (shadcn/ui helper) for conditional classes

```tsx
import { cn } from '@/lib/utils'

// ✅ Use tokens
<div className="bg-surface border border-border rounded-lg p-4">
  <h3 className="text-text font-semibold">{title}</h3>
  <p className="text-text-secondary text-sm">{description}</p>
</div>

// ✅ Conditional classes with cn()
<button className={cn(
  'bg-brand text-white px-4 py-2 rounded-md',
  isDisabled && 'opacity-50 cursor-not-allowed'
)}>
  Submit
</button>

// ❌ Never hardcode colors
<div className="bg-[#1a1a1a]">
```

---

## shadcn/ui Components

Components in `components/ui/` are generated by the shadcn/ui CLI. **Do not edit them directly.** If you need to customize behavior, wrap the component:

```tsx
// components/shared/AppButton.tsx — wrapping shadcn Button
import { Button, type ButtonProps } from '@/components/ui/button'
import { cn } from '@/lib/utils'

export function AppButton({ className, ...props }: ButtonProps) {
  return (
    <Button
      className={cn('font-semibold', className)}
      {...props}
    />
  )
}
```

---

## State Management

- **Local UI state:** `useState` / `useReducer`
- **Server state / data fetching:** React Server Components or `fetch` with revalidation
- **Global auth state:** React Context (`AuthContext`) — see Issue #17
- **Complex client state:** Zustand (if needed in the future)
- **Avoid** prop drilling more than 2 levels deep — use Context

---

## Code Checklist

Before committing:
- [ ] No `console.log` or `debugger` statements
- [ ] All props typed with explicit interfaces
- [ ] No hardcoded hex colors (use Tailwind tokens)
- [ ] `'use client'` used only where necessary
- [ ] `npm run lint` passes without errors
- [ ] `npx tsc --noEmit` passes (no TypeScript errors)
- [ ] Loading and error states handled in interactive components
