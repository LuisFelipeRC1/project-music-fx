import { AlbumReview, TrackReview } from "@/types";
import StarRating from "./StarRating";
import Link from "next/link";

interface Props {
  review: AlbumReview | TrackReview;
  showSubject?: boolean;
}

function timeAgo(dateStr: string): string {
  const diff = Date.now() - new Date(dateStr).getTime();
  const days = Math.floor(diff / 86400000);
  if (days === 0) return "today";
  if (days === 1) return "yesterday";
  if (days < 30) return `${days} days ago`;
  const months = Math.floor(days / 30);
  if (months < 12) return `${months} months ago`;
  return `${Math.floor(months / 12)} years ago`;
}

function isAlbumReview(review: AlbumReview | TrackReview): review is AlbumReview {
  return "album" in review;
}

export default function ReviewCard({ review, showSubject = false }: Props) {
  return (
    <div className="bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl p-5 hover:border-[#3a3a3a] transition-colors">
      <div className="flex items-start justify-between gap-3">
        <div className="flex items-start gap-3 flex-1 min-w-0">
          {/* Avatar */}
          <Link href={`/profile/${review.user.id}`} className="shrink-0">
            <div className="w-9 h-9 rounded-full bg-[#1db954] flex items-center justify-center text-white font-bold text-sm">
              {review.user.displayName[0].toUpperCase()}
            </div>
          </Link>

          <div className="flex-1 min-w-0">
            <div className="flex flex-wrap items-center gap-2">
              <Link
                href={`/profile/${review.user.id}`}
                className="text-white font-semibold text-sm hover:text-[#1db954] transition-colors"
              >
                {review.user.displayName}
              </Link>
              <StarRating rating={review.rating} size="sm" />
              <span className="text-xs text-[#a0a0a0]">{timeAgo(review.createdAt)}</span>
            </div>

            {showSubject && isAlbumReview(review) && (
              <Link href={`/album/${review.album.id}`} className="text-xs text-[#1db954] hover:underline">
                {review.album.title} — {review.album.artist.name}
              </Link>
            )}

            <p className="mt-2 text-sm text-[#a0a0a0] leading-relaxed">{review.content}</p>

            {review.likesCount > 0 && (
              <p className="mt-2 text-xs text-[#a0a0a0]">
                ♥ {review.likesCount} {review.likesCount === 1 ? "like" : "likes"}
              </p>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
