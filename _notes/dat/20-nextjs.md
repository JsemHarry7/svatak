# DAT 20 — Next.js (SSR/CSR/SSG/ISR, App Router)

> **Cíl:** umět postavit Next.js aplikaci s App Routerem, vědět rozdíl rendering strategií, znát Server vs Client Components.
> **Co to je:** **React framework** od Vercelu. Postavený nad Reactem, přidává routing, server-side rendering, image optimization, atd.
> **Verze:** Next.js 14+ (App Router default), Next 15 přidal React 19 podporu.

---

## React vs Next.js

**React** = **knihovna** (jen UI, ty řešíš routing, SSR, build, atd.)
**Next.js** = **framework** postavený na Reactu (obsahuje řešení toho všeho out-of-box)

```
Next.js ────────────────────────────┐
├── React (UI knihovna)              │
├── Routing (file-based)             │
├── SSR / SSG / ISR (server render)  │
├── Image optimization               │
├── API routes (backend uvnitř)      │
├── Middleware                       │
└── Build optimalizace               │
```

**Kdy použít:**
- **Next.js** pro **web aplikace** (SEO, server logic, full-stack)
- **Vite + React** pro **SPA** (admin dashboard, interní tool — žádné SEO potřeba)

---

## Setup

```bash
npx create-next-app@latest moje-aplikace
# Otázky:
#   TypeScript? Yes
#   ESLint? Yes
#   Tailwind? Yes
#   src/? Yes
#   App Router? Yes (default)
#   Turbopack? Yes (rychlejší dev)
#   import alias? Yes (@/*)

cd moje-aplikace
npm run dev   # http://localhost:3000
```

---

## App Router (Next 13+)

Souborový systém **= routing**. Složka `app/` je root.

### Konvenční soubory

| Soubor | Role |
|---|---|
| `app/page.tsx` | UI pro route `/` |
| `app/about/page.tsx` | UI pro route `/about` |
| `app/users/[id]/page.tsx` | Dynamic route `/users/5` (id = "5") |
| `app/layout.tsx` | Layout pro celý strom (`<html>`, `<body>`) |
| `app/about/layout.tsx` | Nested layout pro `/about/*` |
| `app/loading.tsx` | UI během načítání |
| `app/error.tsx` | UI při chybě |
| `app/not-found.tsx` | 404 stránka |
| `app/api/users/route.ts` | API endpoint (`GET /api/users`) |

### Příklad struktury

```
app/
├── layout.tsx          ← root layout (vždy renderuje)
├── page.tsx            ← /
├── loading.tsx         ← loading UI
├── error.tsx           ← error UI
├── about/
│   └── page.tsx        ← /about
└── users/
    ├── page.tsx        ← /users
    ├── [id]/
    │   └── page.tsx    ← /users/:id
    └── new/
        └── page.tsx    ← /users/new
```

### Root layout (povinné)

```tsx
// app/layout.tsx
export default function RootLayout({ children }: { children: React.ReactNode }) {
    return (
        <html lang="cs">
            <body>
                <nav>...</nav>
                {children}
            </body>
        </html>
    );
}
```

### Page

```tsx
// app/about/page.tsx
export default function AboutPage() {
    return <h1>O aplikaci</h1>;
}
```

### Dynamic route + params

```tsx
// app/users/[id]/page.tsx
export default function UserPage({ params }: { params: { id: string } }) {
    return <h1>User {params.id}</h1>;
}
```

⚠️ **V Next 15** je `params` Promise: `params: Promise<{ id: string }>` + `const { id } = await params;`.

---

## Rendering strategies (klíč Next.js)

| Strategie | Kdy se HTML generuje | Use case |
|---|---|---|
| **SSG** (Static Site Generation) | **Při buildu**, jeden statický HTML | Blog, dokumentace, marketing — málo se mění |
| **SSR** (Server-Side Rendering) | **Při každém requestu**, server vygeneruje | Personalizovaný obsah (dashboard, news) |
| **ISR** (Incremental Static Regen.) | Při buildu, ale **re-builduje po N sekundách** | E-shop produkty (mění se občas) |
| **CSR** (Client-Side Rendering) | **V browseru po načtení JS** | SPA-like části (interaktivní formuláře) |

### V App Routeru defaultně SSR/SSG

V Next 14 App Router je default **Server Components** (renderují se na serveru). Klient dostává hotové HTML.

### Force CSR / SSR

```tsx
// SSG (default pokud žádné fetch s no-cache)
export default async function Page() { /* ... */ }

// SSR (každý request)
export const dynamic = "force-dynamic";

// ISR (re-build po 60 sekundách)
export const revalidate = 60;
```

---

## Server Components vs Client Components

### Server Components (default)

Renderují se **na serveru**. Klient dostává hotové HTML, **ne JS**.

**Výhody:**
- Menší JS bundle (rychlejší load)
- **Mohou volat DB/API přímo přes `async/await`**
- Lepší SEO

**Omezení:**
- **NEMAJÍ hooky** (useState, useEffect, ...)
- Nemůžou používat browser API (`window`, `localStorage`)
- Nemůžou mít event handlery (`onClick`, `onChange`)

```tsx
// app/users/page.tsx — Server Component (default)
async function UsersPage() {
    const users = await db.users.findMany();   // ← přímý DB call
    return <ul>{users.map(u => <li key={u.id}>{u.name}</li>)}</ul>;
}
```

### Client Components

Renderují se **v browseru** (jako klasický React). Mají všechno (hooky, eventy, browser API).

**Aktivace:** první řádek souboru `"use client";`.

```tsx
// app/components/Counter.tsx
"use client";

import { useState } from "react";

export default function Counter() {
    const [n, setN] = useState(0);
    return <button onClick={() => setN(n + 1)}>{n}</button>;
}
```

### Mixing

- **Server Component může renderovat Client Component** uvnitř
- **Client Component NEmůže renderovat Server Component** přímo (jen jako `children` prop)
- Hranice je daná `"use client";` direktivou — vše importované do client se stává client

**Pravidlo:** **drž Server Components co nejvíc**, použij Client jen pro interaktivitu.

---

## Data fetching v App Router

### Server Component s async/await

```tsx
async function ProductPage({ params }: { params: { id: string } }) {
    const product = await fetch(`https://api.com/products/${params.id}`).then(r => r.json());
    return <div>{product.name}</div>;
}
```

**Fetch cache (Next 14):**
```tsx
fetch("...", { cache: "force-cache" });    // SSG
fetch("...", { cache: "no-store" });       // SSR
fetch("...", { next: { revalidate: 60 } }); // ISR
```

⚠️ **Next 15 změnil default** — fetch už není cached defaultně, musíš explicitně.

### Client Component (klasický fetch v useEffect)

```tsx
"use client";
useEffect(() => {
    fetch(...).then(...)
}, []);
```

…nebo lépe **React Query / SWR** pro client-side data.

---

## API Routes

```tsx
// app/api/users/route.ts
import { NextResponse } from "next/server";

export async function GET() {
    const users = await db.users.findMany();
    return NextResponse.json(users);
}

export async function POST(request: Request) {
    const body = await request.json();
    const user = await db.users.create({ data: body });
    return NextResponse.json(user, { status: 201 });
}
```

Volání z frontu: `fetch("/api/users")` (same-origin, žádný CORS).

---

## `<Link>` a `<Image>` (Next-specific)

### Link (client-side navigace, žádný reload)

```tsx
import Link from "next/link";

<Link href="/about">O aplikaci</Link>
<Link href={`/users/${user.id}`}>{user.name}</Link>
```

Stejné jako React Router `<Link>`, ale s **`href`** ne `to`.

### Image (optimalizace)

```tsx
import Image from "next/image";

<Image src="/hero.jpg" width={1200} height={600} alt="..." />
```

Next automaticky:
- Lazy load
- Responsive sizing
- WebP/AVIF konverze
- Velikost optimalizovaná pro device

---

## Pages Router (legacy)

Před App Routerem (Next 12-) byl **Pages Router** v `pages/` složce. Stále funguje, ale **App Router je default**.

| Pages Router | App Router |
|---|---|
| `pages/about.tsx` | `app/about/page.tsx` |
| `pages/users/[id].tsx` | `app/users/[id]/page.tsx` |
| `getServerSideProps` | Server Component s async |
| `getStaticProps` | Server Component s cached fetch |
| `_app.tsx` | `layout.tsx` |
| `_document.tsx` | `layout.tsx` (vyšší úroveň) |

**Pro maturitu zmínit oba, ale fokus App Router** (moderní).

---

## Časté chytáky komise

1. **Next.js = framework**, React = knihovna. Knihovna ji můžeš volat, framework volá tvůj kód.
2. **4 rendering strategy:** SSG, SSR, ISR, CSR. Vědět rozdíl + use cases.
3. **Server Components default v App Router** — bez hooks, ale můžou async fetch.
4. **`"use client"`** = direktiva pro Client Component. Bez ní je default Server.
5. **File-based routing** — `app/about/page.tsx` = `/about`.
6. **Dynamic route `[id]`** s `params` (Next 15: `await params`).
7. **`<Link href="...">`** ne `to=` (jako React Router) — Next používá `href`.
8. **API Routes** = `app/api/.../route.ts` — full-stack v jednom projektu.

---

## Q&A pro komisi

**Q:** Co je Next.js?
**A:** React framework od Vercelu. Postavený nad Reactem, přidává file-based routing, server-side rendering, image optimization, API routes — full-stack řešení out-of-box.

**Q:** Rozdíl SSR a SSG?
**A:** **SSG** = HTML se generuje **při buildu** (statický pro každého stejný). **SSR** = HTML se generuje **při každém requestu** na serveru (personalizované).

**Q:** Co je ISR?
**A:** **Incremental Static Regeneration** — SSG, který se **re-builduje v intervalu** (např. 60s). Best of both: rychlost SSG, čerstvost SSR.

**Q:** Server Components vs Client Components?
**A:** **Server** = render na serveru, klient dostane HTML, **bez hooks**, můžou async fetch. **Client** = `"use client"` direktiva, klasický React s hooks a browser API. Mixing povolen, hranice daná direktivou.

**Q:** Proč Next nad čistým Reactem?
**A:** **SEO** (server render), **performance** (statické HTML, image optimization), **full-stack** (API routes bez samostatného backendu), **DX** (file routing, hot reload).

**Q:** Jak funguje routing?
**A:** **File-based**. Soubor `app/about/page.tsx` = route `/about`. Dynamic `app/users/[id]/page.tsx` = `/users/:id`. Layout sdílený přes `layout.tsx` v každé úrovni.
