export default function SearchPage() {
  return (
    <div>
      <h1 className="text-2xl font-bold text-zinc-100 mb-6">Search</h1>
      <div className="relative mb-8">
        <input
          type="text"
          placeholder="Search artists, albums, tracks, or users..."
          className="w-full bg-zinc-900 border border-zinc-700 rounded-full px-6 py-3 text-zinc-100 placeholder-zinc-500 focus:outline-none focus:border-emerald-500 transition-colors"
        />
        <span className="absolute right-4 top-1/2 -translate-y-1/2 text-zinc-500 text-lg">
          🔍
        </span>
      </div>

      <div className="grid grid-cols-2 md:grid-cols-4 gap-3">
        <CategoryCard label="Artists" icon="🎤" />
        <CategoryCard label="Albums" icon="💿" />
        <CategoryCard label="Tracks" icon="🎵" />
        <CategoryCard label="Users" icon="👤" />
      </div>
    </div>
  );
}

function CategoryCard({ label, icon }: { label: string; icon: string }) {
  return (
    <button className="bg-zinc-900 border border-zinc-800 hover:border-emerald-500 rounded-xl p-6 flex flex-col items-center gap-2 transition-colors cursor-pointer">
      <span className="text-3xl">{icon}</span>
      <span className="text-sm font-medium text-zinc-300">{label}</span>
    </button>
  );
}
