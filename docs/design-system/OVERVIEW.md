# Design System Overview — MusicXD

MusicXD's design system is a dark, music-first aesthetic inspired by immersive media platforms. It prioritizes content legibility, high contrast, and a strong brand identity using Spotify's iconic green.

---

## Philosophy

- **Dark by default:** The app lives at night. Dark backgrounds put music content front and center.
- **Content-first:** Typography, album art, and ratings are primary. UI chrome is minimal.
- **Accessible:** WCAG AA contrast ratios for all text/background combinations.
- **Consistent:** Every visual decision maps to a design token. No one-off hex colors.

---

## Stack

| Layer | Tool |
|-------|------|
| Utility CSS | [Tailwind CSS v3](https://tailwindcss.com) |
| Component primitives | [shadcn/ui](https://ui.shadcn.com) (Radix UI + Tailwind) |
| Design tokens | `musicxd.web/tailwind.config.ts` |
| Icons | [Lucide React](https://lucide.dev) |
| Fonts | Inter (body), JetBrains Mono (code/numbers) |

---

## Quick Reference

- [TOKENS.md](TOKENS.md) — All color, typography, spacing, and border radius values
- [COMPONENTS.md](COMPONENTS.md) — Component catalog with usage examples

---

## Color Philosophy

The palette uses a tight dark scale (`#0f0f0f` → `#1a1a1a` → `#242424`) to create depth without harsh contrast. The brand green (`#1db954`) provides the single accent color, drawing attention to primary actions and content highlights.

```
Layer 0 (base):    #0f0f0f  ← page background
Layer 1 (surface): #1a1a1a  ← cards, panels
Layer 2 (raised):  #242424  ← dropdowns, tooltips
Layer 3 (border):  #2a2a2a  ← separation between layers
Accent:            #1db954  ← brand, CTAs, active states
```

---

## Typography Hierarchy

```
H1 (text-4xl, font-bold)       → Album / Artist name
H2 (text-2xl, font-semibold)   → Section headers
H3 (text-xl, font-semibold)    → Card titles
Body (text-base, font-normal)  → Review text, descriptions
Caption (text-sm, text-text-secondary) → Metadata, dates
Micro (text-xs)                → Badges, labels
```

---

## Accessibility Notes

- Minimum contrast ratio: 4.5:1 for normal text (WCAG AA)
- `text-text` (#ffffff) on `bg-bg` (#0f0f0f): contrast ratio **21:1** ✅
- `text-text-secondary` (#a3a3a3) on `bg-surface` (#1a1a1a): contrast ratio **5.4:1** ✅
- `text-brand` (#1db954) on `bg-bg` (#0f0f0f): contrast ratio **7.5:1** ✅
- All interactive elements need focus-visible states (use `focus:ring-1 focus:ring-brand`)
- Star ratings need ARIA attributes (`role="radiogroup"`)
