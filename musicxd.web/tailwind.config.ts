import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      colors: {
        // Backgrounds (dark layered scale)
        bg:               "#0f0f0f",
        surface:          "#1a1a1a",
        "surface-raised": "#242424",

        // Borders
        border:           "#2a2a2a",

        // Brand (Spotify green)
        brand:            "#1db954",
        "brand-hover":    "#1ed760",
        "brand-muted":    "#158a3e",

        // Text
        text:             "#ffffff",
        "text-secondary": "#a3a3a3",
        "text-muted":     "#737373",

        // Semantic
        error:            "#e53e3e",
        warning:          "#d97706",
        // Note: success intentionally shares brand value (#1db954).
        // In MusicXD's music context, green carries both brand and success meaning.
        // If brand color changes, evaluate whether success should update too.
        success:          "#1db954",
      },
      fontFamily: {
        sans: ["Inter", "system-ui", "sans-serif"],
        mono: ["JetBrains Mono", "monospace"],
      },
      borderRadius: {
        sm:   "4px",
        md:   "8px",
        lg:   "12px",
        xl:   "16px",
        full: "9999px",
      },
    },
  },
  plugins: [],
};

export default config;
