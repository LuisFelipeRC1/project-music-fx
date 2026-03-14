"use client";

interface Props {
  rating: number; // 1-5 with decimal support
  size?: "sm" | "md" | "lg";
  interactive?: boolean;
  onRate?: (rating: number) => void;
}

const sizeMap = {
  sm: "w-3.5 h-3.5",
  md: "w-5 h-5",
  lg: "w-7 h-7",
};

export default function StarRating({ rating, size = "md", interactive = false, onRate }: Props) {
  const stars = [1, 2, 3, 4, 5];

  return (
    <div className="flex items-center gap-0.5" aria-label={`Rating: ${rating} out of 5`}>
      {stars.map((star) => {
        const filled = rating >= star;
        const partial = !filled && rating > star - 1;
        const fillPct = partial ? Math.round((rating - (star - 1)) * 100) : 0;

        return (
          <span
            key={star}
            className={`relative inline-block ${sizeMap[size]} ${interactive ? "cursor-pointer" : ""}`}
            onClick={() => interactive && onRate?.(star)}
          >
            {/* Background (empty) star */}
            <svg
              viewBox="0 0 20 20"
              fill="none"
              className={`absolute inset-0 ${sizeMap[size]} text-[#2a2a2a]`}
            >
              <path
                d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"
                fill="currentColor"
              />
            </svg>

            {/* Foreground (filled) star — clipped to fillPct or 100% */}
            <svg
              viewBox="0 0 20 20"
              fill="none"
              className={`relative ${sizeMap[size]} text-[#1db954]`}
              style={{ clipPath: filled ? "none" : `inset(0 ${100 - fillPct}% 0 0)` }}
            >
              <path
                d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"
                fill="currentColor"
              />
            </svg>
          </span>
        );
      })}
      <span className="ml-1 text-xs text-[#a0a0a0]">{rating.toFixed(1)}</span>
    </div>
  );
}
