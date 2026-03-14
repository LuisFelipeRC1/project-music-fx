import Link from "next/link";

export default function Home() {
  return (
    <div className="flex flex-col items-center justify-center py-20 text-center gap-8">
      <div>
        <h1 className="text-5xl font-extrabold text-zinc-100 mb-4">
          🎧 <span className="text-emerald-400">MusicXD</span>
        </h1>
        <p className="text-xl text-zinc-400 max-w-xl">
          Discover music through community. Rate songs, review albums, follow
          friends, and explore what people are listening to.
        </p>
      </div>

      <div className="flex gap-4">
        <Link
          href="/register"
          className="bg-emerald-500 hover:bg-emerald-400 text-zinc-950 font-semibold px-8 py-3 rounded-full text-lg transition-colors"
        >
          Get Started
        </Link>
        <Link
          href="/feed"
          className="border border-zinc-700 hover:border-zinc-500 text-zinc-300 px-8 py-3 rounded-full text-lg transition-colors"
        >
          Explore Feed
        </Link>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mt-8 w-full max-w-3xl">
        <FeatureCard
          icon="⭐"
          title="Rate &amp; Review"
          description="Rate your favorite albums and tracks. Share your thoughts with the community."
        />
        <FeatureCard
          icon="👥"
          title="Follow Friends"
          description="Follow friends and see what they're listening to in your activity feed."
        />
        <FeatureCard
          icon="🔗"
          title="Spotify Sync"
          description="Connect your Spotify account to import your top tracks and listening history."
        />
      </div>
    </div>
  );
}

function FeatureCard({
  icon,
  title,
  description,
}: {
  icon: string;
  title: string;
  description: string;
}) {
  return (
    <div className="bg-zinc-900 border border-zinc-800 rounded-xl p-6 text-left">
      <div className="text-3xl mb-3">{icon}</div>
      <h3 className="font-semibold text-zinc-100 mb-2">{title}</h3>
      <p className="text-sm text-zinc-400">{description}</p>
    </div>
  );
}
