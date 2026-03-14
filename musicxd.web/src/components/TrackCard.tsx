import { Track } from "@/types";
import Link from "next/link";
import StarRating from "./StarRating";

interface Props {
  track: Track;
  rank?: number;
}

function formatDuration(ms: number): string {
  const totalSeconds = Math.floor(ms / 1000);
  const minutes = Math.floor(totalSeconds / 60);
  const seconds = totalSeconds % 60;
  return `${minutes}:${seconds.toString().padStart(2, "0")}`;
}

export default function TrackCard({ track, rank }: Props) {
  return (
    <Link href={`/track/${track.id}`}>
      <div className="bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl p-4 flex items-center gap-4 hover:border-[#1db954] transition-colors cursor-pointer group">
        {rank !== undefined && (
          <span className="text-[#a0a0a0] text-sm font-mono w-6 text-center shrink-0">{rank}</span>
        )}

        {/* Album thumbnail */}
        <div className="w-12 h-12 rounded-lg overflow-hidden bg-[#2a2a2a] shrink-0">
          {track.album.coverUrl ? (
            <img src={track.album.coverUrl} alt={track.album.title} className="w-full h-full object-cover" />
          ) : (
            <div className="w-full h-full flex items-center justify-center text-[#a0a0a0]">
              <svg className="w-6 h-6" fill="currentColor" viewBox="0 0 24 24">
                <path d="M12 3v10.55c-.59-.34-1.27-.55-2-.55-2.21 0-4 1.79-4 4s1.79 4 4 4 4-1.79 4-4V7h4V3h-6z" />
              </svg>
            </div>
          )}
        </div>

        <div className="flex-1 min-w-0">
          <p className="text-white font-semibold text-sm truncate group-hover:text-[#1db954] transition-colors">
            {track.title}
          </p>
          <p className="text-[#a0a0a0] text-xs truncate mt-0.5">
            {track.artist.name} · {track.album.title}
          </p>
          {track.averageRating !== undefined && (
            <div className="mt-1">
              <StarRating rating={track.averageRating} size="sm" />
            </div>
          )}
        </div>

        <span className="text-[#a0a0a0] text-xs shrink-0">{formatDuration(track.durationMs)}</span>
      </div>
    </Link>
  );
}
