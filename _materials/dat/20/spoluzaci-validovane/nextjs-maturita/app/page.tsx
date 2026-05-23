// app/page.tsx
// ✅ Server Component – nevyžaduje "use client"
// Vhodné pro statický obsah, dobrý pro SEO

import Link from "next/link";

export default function HomePage(): React.JSX.Element {
  return (
    <div>
      <h1>Vítej v Next.js demo aplikaci 👋</h1>
      <p>Tato aplikace ukazuje rozdíl mezi Server a Client komponenty.</p>

      <ul>
        <li>
          {/* Link zajišťuje client-side routing – stránka se nepřenačte celá */}
          <Link href="/dashboard">🕐 Dashboard (Client Component)</Link>
        </li>
        <li>
          <Link href="/blog/prvni-clanek">📝 Blog (dynamická cesta)</Link>
        </li>
      </ul>
    </div>
  );
}
