export default function FeedPage() {
  return (
    <div>
      <h1 className="text-2xl font-bold text-zinc-100 mb-6">Activity Feed</h1>
      <div className="space-y-4">
        <FeedCard
          username="alice"
          action="reviewed"
          target="Blonde by Frank Ocean"
          rating={9}
          time="2 hours ago"
        />
        <FeedCard
          username="bob"
          action="rated"
          target="Good Kid, M.A.A.D City"
          rating={10}
          time="5 hours ago"
        />
        <FeedCard
          username="carol"
          action="started following"
          target="dave"
          time="1 day ago"
        />
      </div>
    </div>
  );
}

function FeedCard({
  username,
  action,
  target,
  rating,
  time,
}: {
  username: string;
  action: string;
  target: string;
  rating?: number;
  time: string;
}) {
  return (
    <div className="bg-zinc-900 border border-zinc-800 rounded-xl p-4 flex items-start gap-4">
      <div className="w-10 h-10 rounded-full bg-emerald-500 flex items-center justify-center text-zinc-950 font-bold shrink-0">
        {username[0].toUpperCase()}
      </div>
      <div className="flex-1">
        <p className="text-zinc-200">
          <span className="font-semibold text-zinc-100">@{username}</span>{" "}
          {action}{" "}
          <span className="text-emerald-400">{target}</span>
          {rating !== undefined && (
            <span className="ml-2 text-yellow-400 font-semibold">
              ★ {rating}/10
            </span>
          )}
        </p>
        <p className="text-xs text-zinc-500 mt-1">{time}</p>
      </div>
    </div>
  );
}
