# UI Component Catalog — MusicXD

All base components come from [shadcn/ui](https://ui.shadcn.com/) — installed in `components/ui/`. Custom components live in `components/shared/` and `components/features/`.

---

## Installing Components

```bash
# From musicxd.web/ directory
npx shadcn@latest add <component-name>

# Examples:
npx shadcn@latest add button
npx shadcn@latest add card dialog
```

---

## Installed Components

### Core

| Component | Import | Usage |
|-----------|--------|-------|
| `Button` | `@/components/ui/button` | All clickable actions |
| `Input` | `@/components/ui/input` | Text inputs |
| `Card` | `@/components/ui/card` | Content containers |
| `Badge` | `@/components/ui/badge` | Status labels, tags |
| `Avatar` | `@/components/ui/avatar` | User profile pictures |
| `Separator` | `@/components/ui/separator` | Visual dividers |

### Forms

| Component | Import | Usage |
|-----------|--------|-------|
| `Form` | `@/components/ui/form` | Form wrapper with react-hook-form |
| `Label` | `@/components/ui/label` | Form field labels |
| `Textarea` | `@/components/ui/textarea` | Multi-line text input |
| `Select` | `@/components/ui/select` | Dropdown selection |
| `Checkbox` | `@/components/ui/checkbox` | Boolean toggle |

### Feedback

| Component | Import | Usage |
|-----------|--------|-------|
| `Alert` | `@/components/ui/alert` | Inline messages (info, error, warning) |
| `Sonner` (Toast) | `@/components/ui/sonner` | Transient notifications |
| `Skeleton` | `@/components/ui/skeleton` | Loading placeholders |

### Navigation

| Component | Import | Usage |
|-----------|--------|-------|
| `Tabs` | `@/components/ui/tabs` | Tab navigation |
| `DropdownMenu` | `@/components/ui/dropdown-menu` | Context menus |
| `Sheet` | `@/components/ui/sheet` | Mobile drawer/sidebar |

### Overlay

| Component | Import | Usage |
|-----------|--------|-------|
| `Dialog` | `@/components/ui/dialog` | Modal dialogs |
| `Tooltip` | `@/components/ui/tooltip` | Hover hints |
| `Popover` | `@/components/ui/popover` | Inline popups |

---

## Usage Examples

### Button

```tsx
import { Button } from '@/components/ui/button'

// Primary (default)
<Button>Follow</Button>

// Variants
<Button variant="outline">Cancel</Button>
<Button variant="ghost">View Profile</Button>
<Button variant="destructive">Unfollow</Button>

// Sizes
<Button size="sm">Small</Button>
<Button size="lg">Large</Button>

// Loading state
<Button disabled>
  <Loader2 className="mr-2 h-4 w-4 animate-spin" />
  Saving...
</Button>
```

### Card

```tsx
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'

<Card className="bg-surface border-border">
  <CardHeader>
    <CardTitle className="text-text">Album Reviews</CardTitle>
  </CardHeader>
  <CardContent>
    <p className="text-text-secondary">content here</p>
  </CardContent>
</Card>
```

### Avatar

```tsx
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'

<Avatar>
  <AvatarImage src={user.avatarUrl} alt={user.username} />
  <AvatarFallback className="bg-brand text-bg">
    {user.username.slice(0, 2).toUpperCase()}
  </AvatarFallback>
</Avatar>
```

### Toast (Sonner)

```tsx
import { toast } from 'sonner'

// Show toasts
toast.success('Review posted!')
toast.error('Failed to save. Try again.')
toast.info('Following user...')

// In root layout, add the Toaster:
import { Toaster } from '@/components/ui/sonner'
<Toaster theme="dark" />
```

### Skeleton (loading state)

```tsx
import { Skeleton } from '@/components/ui/skeleton'

// Album card skeleton
<div className="bg-surface border border-border rounded-lg p-4">
  <Skeleton className="h-48 w-full rounded-md mb-4" />
  <Skeleton className="h-5 w-3/4 mb-2" />
  <Skeleton className="h-4 w-1/2" />
</div>
```

### Dialog

```tsx
import {
  Dialog, DialogContent, DialogHeader,
  DialogTitle, DialogTrigger
} from '@/components/ui/dialog'

<Dialog>
  <DialogTrigger asChild>
    <Button>Write Review</Button>
  </DialogTrigger>
  <DialogContent className="bg-surface border-border">
    <DialogHeader>
      <DialogTitle className="text-text">Your Review</DialogTitle>
    </DialogHeader>
    {/* form content */}
  </DialogContent>
</Dialog>
```

---

## Custom Components

### `components/shared/StarRating.tsx`
Interactive 5-star rating component. Accessible (ARIA radio group). See Issue #26 for accessibility improvements.

### `components/shared/Navbar.tsx`
Top navigation bar. Contains logo, navigation links, search, and user avatar dropdown.

### `components/features/album/AlbumCard.tsx`
Album preview card showing cover image, title, artist, and average rating.

### `components/features/track/TrackCard.tsx`
Track preview card showing track info and rating.

### `components/features/feed/ActivityCard.tsx`
Activity feed item showing user action (reviewed album, followed user, etc.).

### `components/features/review/ReviewCard.tsx`
Displays a single review with user info, rating, and review text.

---

## Don't Edit `components/ui/`

Files in `components/ui/` are managed by the shadcn/ui CLI. Editing them directly means your changes will be overwritten on the next `npx shadcn@latest add` or update.

To customize a component:
1. Create a wrapper in `components/shared/`
2. Import and extend the base component
3. Never modify the original file in `components/ui/`
