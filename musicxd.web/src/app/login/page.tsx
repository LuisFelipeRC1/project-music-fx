"use client";

import Link from "next/link";
import { useState } from "react";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError("");
    // Mock login — replace with api.login(email, password)
    await new Promise((r) => setTimeout(r, 800));
    setLoading(false);
    setError("Demo: API not connected yet.");
  };

  return (
    <div className="min-h-[80vh] flex items-center justify-center">
      <div className="w-full max-w-md">
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold text-white">
            Welcome back to <span className="text-[#1db954]">MusicXD</span>
          </h1>
          <p className="text-[#a0a0a0] mt-2 text-sm">Log in to continue your music journey</p>
        </div>

        <div className="bg-[#1a1a1a] border border-[#2a2a2a] rounded-2xl p-8">
          <form onSubmit={handleSubmit} className="flex flex-col gap-5">
            <div>
              <label className="block text-sm font-medium text-[#a0a0a0] mb-1.5">Email</label>
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
                placeholder="you@example.com"
                className="w-full bg-[#0f0f0f] border border-[#2a2a2a] rounded-lg px-4 py-3 text-white placeholder-[#555] text-sm focus:outline-none focus:border-[#1db954] transition-colors"
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-[#a0a0a0] mb-1.5">Password</label>
              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
                placeholder="••••••••"
                className="w-full bg-[#0f0f0f] border border-[#2a2a2a] rounded-lg px-4 py-3 text-white placeholder-[#555] text-sm focus:outline-none focus:border-[#1db954] transition-colors"
              />
            </div>

            {error && (
              <p className="text-red-400 text-sm bg-red-400/10 border border-red-400/20 rounded-lg px-4 py-2">
                {error}
              </p>
            )}

            <button
              type="submit"
              disabled={loading}
              className="bg-[#1db954] hover:bg-[#17a349] disabled:opacity-50 text-white font-semibold py-3 rounded-full transition-colors text-sm"
            >
              {loading ? "Logging in…" : "Log in"}
            </button>
          </form>

          <p className="mt-6 text-center text-sm text-[#a0a0a0]">
            Don&apos;t have an account?{" "}
            <Link href="/register" className="text-[#1db954] hover:underline font-medium">
              Sign up
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
}
