# Design Tokens — MusicXD

Design tokens are the single source of truth for all visual decisions. All values are defined in `musicxd.web/tailwind.config.ts` and available as Tailwind utility classes.

---

## Colors

### Usage in code

```tsx
// Backgrounds
<div className="bg-bg">           // Page background
<div className="bg-surface">      // Cards, modals, panels
<div className="bg-surface-raised"> // Dropdowns, tooltips, popovers

// Text
<p className="text-text">         // Primary text
<p className="text-text-secondary"> // Secondary/supporting text
<p className="text-text-muted">   // Placeholders, hints, disabled

// Brand (primary actions)
<button className="bg-brand hover:bg-brand-hover text-bg">

// Borders
<div className="border border-border">
```

### Color Palette

| Token | Class | Hex | Usage |
|-------|-------|-----|-------|
| Background | `bg-bg` | `#0f0f0f` | Page/app background |
| Surface | `bg-surface` | `#1a1a1a` | Cards, modals, sidebars |
| Surface Raised | `bg-surface-raised` | `#242424` | Dropdowns, tooltips, popovers |
| Border | `border-border` | `#2a2a2a` | Dividers, input borders, separators |
| Brand | `bg-brand` / `text-brand` | `#1db954` | Primary CTAs, active states, links |
| Brand Hover | `hover:bg-brand-hover` | `#1ed760` | Hover state for brand elements |
| Brand Muted | `bg-brand-muted` | `#158a3e` | Disabled brand, secondary brand |
| Text | `text-text` | `#ffffff` | Primary body text, headings |
| Text Secondary | `text-text-secondary` | `#a3a3a3` | Supporting text, metadata, labels |
| Text Muted | `text-text-muted` | `#737373` | Placeholders, hints, disabled text |
| Error | `text-error` / `bg-error` | `#e53e3e` | Error messages, destructive actions |
| Warning | `text-warning` | `#d97706` | Warning messages, caution states |
| Success | `text-success` | `#1db954` | Success states |

> **Note on Success vs Brand:** `--color-success` and `--color-brand` intentionally share the same value (`#1db954`). In MusicXD's dark music theme, the brand green doubles as the success color. This is a deliberate product decision — the green already carries positive connotation in the music context. If the brand color ever changes, revisit whether success should be updated independently.

---

## Typography

### Fonts

| Role | Font | Fallback |
|------|------|---------|
| Body / UI | Inter | `system-ui`, `sans-serif` |
| Code / Numbers | JetBrains Mono | `monospace` |

Apply with Tailwind: `font-sans`, `font-mono`

### Type Scale

| Token | Size | Usage |
|-------|------|-------|
| `text-xs` | 12px | Labels, badges, micro-copy |
| `text-sm` | 14px | Secondary text, metadata, captions |
| `text-base` | 16px | Default body text |
| `text-lg` | 18px | Large body, card subtitles |
| `text-xl` | 20px | Section headings, card titles |
| `text-2xl` | 24px | Page sub-headings |
| `text-3xl` | 30px | Section headers |
| `text-4xl` | 36px | Page titles |
| `text-5xl` | 48px | Hero/display text |

### Font Weights

| Token | Weight | Usage |
|-------|--------|-------|
| `font-normal` | 400 | Body text |
| `font-medium` | 500 | Slightly emphasized text |
| `font-semibold` | 600 | Card titles, UI labels |
| `font-bold` | 700 | Page headings, CTAs |

---

## Spacing

All spacing uses a **4px base grid**. Stick to multiples of 4.

| Value | Tailwind | Usage |
|-------|---------|-------|
| 4px | `p-1` / `m-1` | Micro spacing (icon padding) |
| 8px | `p-2` / `m-2` | Tight spacing (button padding) |
| 12px | `p-3` / `m-3` | Compact components |
| 16px | `p-4` / `m-4` | Default component padding |
| 24px | `p-6` / `m-6` | Card padding, section gaps |
| 32px | `p-8` / `m-8` | Large section spacing |
| 48px | `p-12` / `m-12` | Between major sections |
| 64px | `p-16` / `m-16` | Page-level vertical rhythm |

---

## Border Radius

| Token | Size | Usage |
|-------|------|-------|
| `rounded-sm` | 4px | Subtle rounding (badges, chips) |
| `rounded-md` | 8px | Default buttons, inputs |
| `rounded-lg` | 12px | Cards, panels |
| `rounded-xl` | 16px | Modals, large containers |
| `rounded-full` | 9999px | Avatars, pill badges |

---

## Shadows

Use sparingly in a dark theme — prefer `border-border` over shadows.

| Token | Usage |
|-------|-------|
| `shadow-sm` | Subtle elevation for dropdowns |
| `shadow-md` | Modal/dialog elevation |
| `shadow-lg` | Rarely — for floating elements |

---

## Common Component Patterns

### Card

```tsx
<div className="bg-surface border border-border rounded-lg p-4">
  ...
</div>
```

### Primary Button

```tsx
<button className="bg-brand hover:bg-brand-hover text-bg font-semibold px-4 py-2 rounded-md transition-colors">
  Follow
</button>
```

### Text Input

```tsx
<input className="bg-surface border border-border rounded-md px-3 py-2 text-text placeholder:text-text-muted focus:outline-none focus:ring-1 focus:ring-brand" />
```

### Badge

```tsx
<span className="bg-brand/10 text-brand text-xs font-medium px-2 py-0.5 rounded-full">
  New
</span>
```
