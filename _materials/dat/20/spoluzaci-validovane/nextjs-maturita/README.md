# Maturitní zadání: Next.js – Server vs. Klient rendering

## Téma: Vytvoř mini aplikaci demonstrující rozdíl mezi SSR a CSR

---

## Zadání

Vytvoř Next.js aplikaci v **TypeScriptu**, která bude demonstrovat rozdíl mezi **Server Components** a **Client Components**. Aplikace bude jednoduchý rozcestník se třemi stránkami.

---

## Požadovaná struktura

```
app/
├── page.tsx                 # Domovská stránka (Server Component)
├── layout.tsx               # Root Layout (Server Component)
├── dashboard/
│   └── page.tsx             # Dashboard s hodinami (Client Component)
└── blog/
    └── [slug]/
        └── page.tsx         # Dynamická stránka článku (Server Component)
components/
├── Clock.tsx                # "use client" komponenta s živým časem
└── Navbar.tsx               # Navigační lišta
```

---

## Co musí aplikace umět

### 1. Domovská stránka (`/`)

- Jednoduchý uvítací text
- Odkaz na `/dashboard` a `/blog/prvni-clanek`
- Musí být **Server Component** (žádný `"use client"`)

### 2. Dashboard (`/dashboard`)

- Zobrazí **aktuální čas**, který se každou sekundu aktualizuje
- Hodiny musí být v samostatné komponentě `Clock.tsx`
- `Clock.tsx` **musí obsahovat** direktivu `"use client"` (používá `useState` a `useEffect`)

### 3. Blog stránka (`/blog/[slug]`)

- Zobrazí název článku odvozený z URL parametru `slug`
- Musí být **Server Component**, která přijímá `params` jako prop
- V Next.js 15+ je `params` typu `Promise<{ slug: string }>`, proto je komponenta `async` a používá `await`
- Příklad: `/blog/maturita` zobrazí nadpis „Článek: maturita"

### 4. Navigace

- Komponenta `Navbar.tsx` s odkazy pomocí `<Link>` z Next.js
- Použita na všech stránkách (přes `layout.tsx`)

---

## TypeScript specifika

- Všechny soubory používají příponu `.tsx` místo `.js`
- Komponenty mají explicitní návratový typ `React.JSX.Element`
- Props jsou typovány pomocí rozhraní (`interface`)
- Metadata objekty používají typ `Metadata` z `next`
- Stav v `useState` je typován genericky, např. `useState<string | null>(null)`

---

## Co nesmíš udělat

- Přidat `"use client"` do stránky, která to nepotřebuje
- Použít `<a href="...">` místo `<Link>` pro interní navigaci
- Použít `useState` nebo `useEffect` v Server Componentě
- Ponechat soubory s příponou `.js` – vše musí být `.tsx`

---

## Nápovědy

<details>
<summary>Jak vypadá Client Component s časem v TypeScriptu?</summary>

```tsx
"use client";
import { useState, useEffect } from "react";

export default function Clock(): React.JSX.Element {
  const [time, setTime] = useState<string | null>(null);

  useEffect(() => {
    setTime(new Date().toLocaleTimeString("cs-CZ"));

    const interval: ReturnType<typeof setInterval> = setInterval(() => {
      setTime(new Date().toLocaleTimeString("cs-CZ"));
    }, 1000);

    return () => clearInterval(interval);
  }, []);

  return <p>Aktuální čas: {time ?? "Načítám..."}</p>;
}
```

</details>

<details>
<summary>Jak přijmout dynamický parametr v Server Componentě (Next.js 15+)?</summary>

```tsx
interface BlogPostProps {
  params: Promise<{ slug: string }>;
}

export default async function BlogPost({ params }: BlogPostProps): Promise<React.JSX.Element> {
  const { slug } = await params;
  return <h1>Článek: {slug}</h1>;
}
```

</details>

<details>
<summary>Jak spustit Next.js projekt s TypeScriptem?</summary>

```bash
npx create-next-app@latest moje-app --typescript
cd moje-app
npm run dev
```

</details>

---

## Hodnocení

| Kritérium                                 | Body   |
| ----------------------------------------- | ------ |
| Správná struktura souborů a routování     | 2      |
| Funkční Clock komponenta s `"use client"` | 3      |
| Dynamická blog stránka se `[slug]`        | 2      |
| Navigace pomocí `<Link>`                  | 1      |
| Kód je čitelný a komentovaný             | 2      |
| **Celkem**                                | **10** |

---

## Užitečné zdroje

- [Next.js dokumentace – App Router](https://nextjs.org/docs/app)
- [Server vs. Client Components](https://nextjs.org/docs/app/building-your-application/rendering)
- [next/link](https://nextjs.org/docs/app/api-reference/components/link)
- [TypeScript v Next.js](https://nextjs.org/docs/app/building-your-application/configuring/typescript)
