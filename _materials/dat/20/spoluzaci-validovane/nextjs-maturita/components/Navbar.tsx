// components/Navbar.tsx
// Server Component – statická navigace nevyžaduje "use client"
// Používá <Link> z Next.js pro client-side routing

import Link from "next/link";

export default function Navbar(): React.JSX.Element {
  return (
    <nav style={{
      display: "flex",
      gap: "1.5rem",
      padding: "1rem 2rem",
      background: "#f0f0f0",
      borderBottom: "1px solid #ccc"
    }}>
      {/* <Link> = client-side navigace, bez reloadu stránky */}
      <Link href="/">🏠 Domů</Link>
      <Link href="/dashboard">📊 Dashboard</Link>
      <Link href="/blog/ukazka">📝 Blog</Link>
    </nav>
  );
}
