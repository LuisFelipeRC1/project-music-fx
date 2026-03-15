"use client";

import Link from "next/link";
import { useState } from "react";

export default function Navbar() {
  const [menuOpen, setMenuOpen] = useState(false);
  // Mock auth state – replace with real auth context
  const isLoggedIn = false;

  return (
    <nav className="sticky top-0 z-50 bg-[#0f0f0f] border-b border-[#2a2a2a]">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo */}
          <Link href="/" className="flex items-center gap-2 shrink-0">
            <span className="text-[#1db954] text-2xl font-bold tracking-tight">
              MusicXD
            </span>
          </Link>

          {/* Desktop nav links */}
          <div className="hidden md:flex items-center gap-8">
            <Link href="/" className="text-[#a0a0a0] hover:text-white transition-colors text-sm font-medium">
              Home
            </Link>
            <Link href="/discover" className="text-[#a0a0a0] hover:text-white transition-colors text-sm font-medium">
              Discover
            </Link>
            <Link href="/search" className="text-[#a0a0a0] hover:text-white transition-colors text-sm font-medium">
              Search
            </Link>
          </div>

          {/* Right side */}
          <div className="hidden md:flex items-center gap-3">
            {isLoggedIn ? (
              <Link href="/profile/me">
                <div className="w-9 h-9 rounded-full bg-[#1db954] flex items-center justify-center text-white font-bold text-sm hover:opacity-80 transition-opacity cursor-pointer">
                  U
                </div>
              </Link>
            ) : (
              <>
                <Link
                  href="/login"
                  className="text-[#a0a0a0] hover:text-white text-sm font-medium transition-colors"
                >
                  Log in
                </Link>
                <Link
                  href="/register"
                  className="bg-[#1db954] hover:bg-[#17a349] text-white text-sm font-medium px-4 py-2 rounded-full transition-colors"
                >
                  Sign up
                </Link>
              </>
            )}
          </div>

          {/* Mobile menu button */}
          <button
            className="md:hidden text-[#a0a0a0] hover:text-white"
            onClick={() => setMenuOpen(!menuOpen)}
            aria-label="Toggle menu"
          >
            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              {menuOpen ? (
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
              ) : (
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
              )}
            </svg>
          </button>
        </div>
      </div>

      {/* Mobile menu */}
      {menuOpen && (
        <div className="md:hidden bg-[#1a1a1a] border-t border-[#2a2a2a] px-4 py-4 flex flex-col gap-4">
          <Link href="/" className="text-[#a0a0a0] hover:text-white transition-colors text-sm font-medium" onClick={() => setMenuOpen(false)}>
            Home
          </Link>
          <Link href="/discover" className="text-[#a0a0a0] hover:text-white transition-colors text-sm font-medium" onClick={() => setMenuOpen(false)}>
            Discover
          </Link>
          <Link href="/search" className="text-[#a0a0a0] hover:text-white transition-colors text-sm font-medium" onClick={() => setMenuOpen(false)}>
            Search
          </Link>
          <hr className="border-[#2a2a2a]" />
          <Link href="/login" className="text-[#a0a0a0] hover:text-white text-sm font-medium" onClick={() => setMenuOpen(false)}>
            Log in
          </Link>
          <Link href="/register" className="bg-[#1db954] hover:bg-[#17a349] text-white text-sm font-medium px-4 py-2 rounded-full text-center transition-colors" onClick={() => setMenuOpen(false)}>
            Sign up
          </Link>
        </div>
      )}
    </nav>
  );
}
