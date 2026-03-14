export default function ProfilePage() {
  return (
    <div className="max-w-2xl mx-auto">
      <div className="bg-zinc-900 border border-zinc-800 rounded-xl p-6 mb-6">
        <div className="flex items-start gap-5">
          <div className="w-20 h-20 rounded-full bg-emerald-500 flex items-center justify-center text-zinc-950 font-bold text-3xl shrink-0">
            U
          </div>
          <div className="flex-1">
            <h1 className="text-xl font-bold text-zinc-100">@username</h1>
            <p className="text-zinc-400 mt-1 text-sm">
              Music enthusiast. Lover of jazz, indie, and everything in between.
            </p>
            <div className="flex gap-6 mt-4 text-sm">
              <Stat label="Reviews" value="42" />
              <Stat label="Followers" value="128" />
              <Stat label="Following" value="85" />
            </div>
          </div>
        </div>
      </div>

      <h2 className="text-lg font-semibold text-zinc-100 mb-3">
        Recent Reviews
      </h2>
      <div className="space-y-3">
        <ReviewCard
          album="Blonde"
          artist="Frank Ocean"
          rating={9}
          text="A masterpiece of introspection and sonic beauty."
        />
        <ReviewCard
          album="To Pimp a Butterfly"
          artist="Kendrick Lamar"
          rating={10}
          text="One of the greatest albums of our generation."
        />
      </div>
    </div>
  );
}

function Stat({ label, value }: { label: string; value: string }) {
  return (
    <div>
      <span className="font-bold text-zinc-100">{value}</span>{" "}
      <span className="text-zinc-500">{label}</span>
    </div>
  );
}

function ReviewCard({
  album,
  artist,
  rating,
  text,
}: {
  album: string;
  artist: string;
  rating: number;
  text: string;
}) {
  return (
    <div className="bg-zinc-900 border border-zinc-800 rounded-xl p-4">
      <div className="flex items-center justify-between mb-2">
        <div>
          <p className="font-semibold text-zinc-100">{album}</p>
          <p className="text-xs text-zinc-500">{artist}</p>
        </div>
        <span className="text-yellow-400 font-bold">★ {rating}/10</span>
      </div>
      <p className="text-sm text-zinc-400">{text}</p>
    </div>
  );
}
