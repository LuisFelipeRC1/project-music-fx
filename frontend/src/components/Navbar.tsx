import Link from "next/link";

export default function Navbar() {
  return (
    <nav className="bg-zinc-900 border-b border-zinc-800 sticky top-0 z-50">
      <div className="max-w-6xl mx-auto px-4 flex items-center justify-between h-14">
        <Link href="/" className="text-xl font-bold text-emerald-400 tracking-tight">
          🎧 MusicXD
        </Link>

        <div className="flex items-center gap-6 text-sm text-zinc-400">
          <Link href="/feed" className="hover:text-zinc-100 transition-colors">
            Feed
          </Link>
          <Link href="/search" className="hover:text-zinc-100 transition-colors">
            Search
          </Link>
          <Link href="/profile" className="hover:text-zinc-100 transition-colors">
            Profile
          </Link>
        </div>

        <div className="flex items-center gap-3">
          <Link
            href="/login"
            className="text-sm text-zinc-400 hover:text-zinc-100 transition-colors"
          >
            Login
          </Link>
          <Link
            href="/register"
            className="text-sm bg-emerald-500 hover:bg-emerald-400 text-zinc-950 font-medium px-4 py-1.5 rounded-full transition-colors"
          >
            Sign Up
          </Link>
        </div>
      </div>
    </nav>
  );
}
