import { ActivityFeed } from "@/types";
import StarRating from "./StarRating";
import Link from "next/link";

interface Props {
  activity: ActivityFeed;
}

function timeAgo(dateStr: string): string {
  const diff = Date.now() - new Date(dateStr).getTime();
  const minutes = Math.floor(diff / 60000);
  if (minutes < 60) return `${minutes}m ago`;
  const hours = Math.floor(minutes / 60);
  if (hours < 24) return `${hours}h ago`;
  const days = Math.floor(hours / 24);
  return `${days}d ago`;
}

export default function ActivityCard({ activity }: Props) {
  const { user, type, album, track, targetUser, rating } = activity;

  const actionLabel = () => {
    switch (type) {
      case "reviewed_album":
        return (
          <>
            reviewed{" "}
            <Link href={`/album/${album?.id}`} className="text-white hover:text-[#1db954] font-semibold transition-colors">
              {album?.title}
            </Link>{" "}
            by {album?.artist.name}
          </>
        );
      case "reviewed_track":
        return (
          <>
            reviewed{" "}
            <Link href={`/track/${track?.id}`} className="text-white hover:text-[#1db954] font-semibold transition-colors">
              {track?.title}
            </Link>
          </>
        );
      case "rated_album":
        return (
          <>
            rated{" "}
            <Link href={`/album/${album?.id}`} className="text-white hover:text-[#1db954] font-semibold transition-colors">
              {album?.title}
            </Link>
          </>
        );
      case "rated_track":
        return (
          <>
            rated{" "}
            <Link href={`/track/${track?.id}`} className="text-white hover:text-[#1db954] font-semibold transition-colors">
              {track?.title}
            </Link>
          </>
        );
      case "followed_user":
        return (
          <>
            followed{" "}
            <Link href={`/profile/${targetUser?.id}`} className="text-white hover:text-[#1db954] font-semibold transition-colors">
              {targetUser?.displayName}
            </Link>
          </>
        );
    }
  };

  return (
    <div className="bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl p-4 hover:border-[#3a3a3a] transition-colors">
      <div className="flex items-start gap-3">
        {/* Avatar */}
        <Link href={`/profile/${user.id}`} className="shrink-0">
          <div className="w-10 h-10 rounded-full bg-[#1db954] flex items-center justify-center text-white font-bold text-sm">
            {user.displayName[0].toUpperCase()}
          </div>
        </Link>

        <div className="flex-1 min-w-0">
          <p className="text-sm text-[#a0a0a0]">
            <Link href={`/profile/${user.id}`} className="text-white font-semibold hover:text-[#1db954] transition-colors">
              {user.displayName}
            </Link>{" "}
            {actionLabel()}
          </p>

          {rating !== undefined && (
            <div className="mt-1">
              <StarRating rating={rating} size="sm" />
            </div>
          )}

          {activity.review && "content" in activity.review && (
            <p className="mt-2 text-sm text-[#a0a0a0] line-clamp-2">{activity.review.content}</p>
          )}

          {/* Album cover thumbnail */}
          {album?.coverUrl && (
            <Link href={`/album/${album.id}`}>
              <div className="mt-3 w-16 h-16 rounded-lg overflow-hidden bg-[#2a2a2a] shrink-0">
                <img src={album.coverUrl} alt={album.title} className="w-full h-full object-cover" />
              </div>
            </Link>
          )}
        </div>

        <span className="text-xs text-[#a0a0a0] shrink-0">{timeAgo(activity.createdAt)}</span>
      </div>
    </div>
  );
}
