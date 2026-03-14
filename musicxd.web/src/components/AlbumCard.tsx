import { Album } from "@/types";
import Link from "next/link";
import StarRating from "./StarRating";

interface Props {
  album: Album;
}

export default function AlbumCard({ album }: Props) {
  return (
    <Link href={`/album/${album.id}`}>
      <div className="bg-[#1a1a1a] border border-[#2a2a2a] rounded-xl overflow-hidden hover:border-[#1db954] hover:scale-105 transition-all duration-200 cursor-pointer group">
        {/* Cover */}
        <div className="aspect-square w-full bg-[#2a2a2a] overflow-hidden">
          {album.coverUrl ? (
            <img
              src={album.coverUrl}
              alt={album.title}
              className="w-full h-full object-cover group-hover:opacity-80 transition-opacity"
            />
          ) : (
            <div className="w-full h-full flex items-center justify-center text-[#a0a0a0]">
              <svg className="w-12 h-12" fill="currentColor" viewBox="0 0 24 24">
                <path d="M12 3v10.55c-.59-.34-1.27-.55-2-.55-2.21 0-4 1.79-4 4s1.79 4 4 4 4-1.79 4-4V7h4V3h-6z" />
              </svg>
            </div>
          )}
        </div>

        {/* Info */}
        <div className="p-3">
          <p className="text-white font-semibold text-sm truncate">{album.title}</p>
          <p className="text-[#a0a0a0] text-xs truncate mt-0.5">{album.artist.name}</p>
          <p className="text-[#a0a0a0] text-xs mt-0.5">{album.releaseYear}</p>
          {album.averageRating !== undefined && (
            <div className="mt-2">
              <StarRating rating={album.averageRating} size="sm" />
            </div>
          )}
        </div>
      </div>
    </Link>
  );
}
