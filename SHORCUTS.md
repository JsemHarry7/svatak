# 🛟 SHORCUTS.md — Záchranný playbook pro DAT u zkoušky

> **Jsi ztracený a máš jen to, co bude u zkoušky.** Tohle je seznam toho, co **Visual Studio** umí sám o sobě a jak to využít k tomu, aby ses dopracoval k funkčnímu minimu.

> **Repo se NEPŘENÁŠÍ.** Žádné `_practice/`, `_notes/`, `_materials/` u zkoušky nemáš. Máš **VS, dokumentaci, internet (nebo offline `_docs/`)** a 30 minut. Tahle pravidla platí tady.

> **Pravidlo č. 1:** funkční minimum > rozkmnuvšlý ambiciózní pokus.

---

## 🎒 Co máš k dispozici (ŽÁDNÝ INTERNET)

### 1. Visual Studio 2022
- **File → New → Project** — desítky šablon hotových (ASP.NET, Console, Blazor, Class Library...)
- **Add → New Scaffolded Item** — vygeneruje Razor Pages, Controllery, Views z modelu **ZA TEBE**
- **Add → New Item** — single soubor (Razor Page, Class, Interface)
- **NuGet Package Manager** — instalace knihoven z GUI (⚠️ vyžaduje internet pro stahování — **balíčky musí být buď nainstalované, nebo v lokálním NuGet cache**)
- **IntelliSense** — autocomplete (Ctrl+Space), Quick Actions (Ctrl+.)
- **Snippety** — zkratka + Tab Tab → kostra (`prop`, `ctor`, `cw`, `for`, `try`)
- **Solution Explorer** — vidíš strukturu, dvojklik otevírá soubory
- **Error List** (Ctrl+\\, E) — co konkrétně neskompiluje + řádek
- ⚠️ **F1 na klíčovém slově** by otevřelo MS Learn — **bez internetu NEFUNGUJE**. Použij místo toho Zeal nebo PDF.

### 2. Zeal app — offline docsety (POWER TOOL)
**F1 v Zealu = instant search napříč docsety.** Píš jméno API → Enter → dokumentace.

Stažené docsety:
- **HTML** — všechny tagy a atributy
- **CSS** — všechny vlastnosti, hodnoty, pseudoclasses
- **JavaScript** — Array, Object, fetch, Promise, async/await, ...
- **TypeScript** — typed syntax, generics, utility types
- **React** — komponenty, hooky, JSX (offline)
- **Bootstrap 5** — všechny utility classes, komponenty
- **Node.js** — fs, path, http
- **SQLite** — SQL syntax, funkce, ALTER TABLE specifika

### 3. PDF dokumentace v `_docs/`
- **`_docs/asp.net/aspnetcore-docs.pdf`** — ASP.NET Core (Razor Pages, MVC, Web API, EF Core, DI, middleware, autentizace)
- **`_docs/csharp/csharp-docs.pdf`** — C# language reference (syntax, OOP, generics, LINQ, async, ...)
- **Použití:** otevři v Adobe / Edge → **Ctrl+F** pro search v PDF

### 4. Offline markdown clony v `_docs/`
Čteš v jakémkoli editoru / browseru s markdown preview:
- **`_docs/react.dev/src/content/`** — React docs (komponenty, hooky, learn, reference)
- **`_docs/react.cheatsheet/`** — TypeScript+React cheatsheet (props syntax, types — ⭐ zlato)
- **`_docs/typescript.docs/packages/documentation/`** — TS handbook
- **`_docs/prisma.docs/content/`** — Prisma docs
- **`_docs/nextjs.docs/docs/`** — Next.js docs
- **`_docs/vite.docs/docs/`** — Vite docs (po `pnpm docs-build` taky offline static HTML)

### 5. Visual Studio Code (alternativa)
- Lehčí, vhodný pro React/TS bez plného VS Studia
- Extensions: ES7+ React snippets, Prettier, ESLint (musí být předinstalované)
- Terminál uvnitř editoru — pohodlné pro `npm`

---

## 🚨 Krizový rozhodovací strom

```
NEVÍM CO MÁM DĚLAT
       │
       ▼
1) Přečti zadání 2× POMALU. Vypiš keywordy.
   "Co se má vyrobit? Jaké technologie zmiňuje?"
       │
       ▼
2) Visual Studio: File → New → Project
   → Najdi template odpovídající technologii
   → Vygeneruje funkční Hello World, kde můžeš stavět dál
       │
       ▼
3) Spusť hotový template (F5) — ověř že běží
   ⚠️ Bez funkčního baseline neuvidíš ve svých úpravách bug
       │
       ▼
4) Nejmenší krok dál — napiš 5-10 řádků, spusť, ověř.
   Iteruj. NEPIŠ velký blok najednou.
       │
       ▼
5) Nevíš syntax/API?
   - IntelliSense (Ctrl+Space) — VS ti řekne, co dál
   - Zeal F1 — instant search v offline docsetech
   - Ctrl+F v _docs/asp.net/*.pdf nebo _docs/csharp/*.pdf
   - "Ctrl + ." nad červeným podtržením = Quick Fix
       │
       ▼
6) Kompilace selhává?
   - Error List (Ctrl+\, E)
   - PRVNÍ error oprav jako první (kaskáda)
   - Hledej řádek + sloupec, jdi tam, vyřeš
```

---

## 🪄 Visual Studio Templates (File → New → Project)

### C# Console
- **Použij pro:** klasické zadání s file I/O, async/parallel, algoritmy, OOP
- **Co dostaneš:** `Program.cs` s top-level statements (`Console.WriteLine("Hello, World!")`)
- **Build:** F5 (debug) / Ctrl+F5 (run bez debug)

### ASP.NET Core Web App (Razor Pages)
- **Použij pro:** server-side web app, formuláře, CRUD na DB
- **Co dostaneš:** kompletní funkční web s navigací, `Pages/Index.cshtml`, `Pages/Privacy.cshtml`, `Program.cs` s DI setupem, `appsettings.json`
- **Spuštění:** F5 → otevře browser s běžící aplikací
- **Klíčové soubory:**
  - `Pages/X.cshtml` — Razor view (HTML + Razor syntax)
  - `Pages/X.cshtml.cs` — Page Model (C# code-behind s handlery)
  - `Program.cs` — middleware pipeline + DI registrace
  - `appsettings.json` — config

### ASP.NET Core Web API
- **Použij pro:** REST API, JSON endpointy bez UI
- **Co dostaneš:** `WeatherForecastController.cs` jako vzor, Swagger UI na `/swagger`
- **Klíčové:** atributy `[ApiController]`, `[HttpGet]`, `[HttpPost]`, `[Route("api/[controller]")]`

### ASP.NET Core MVC
- **Použij pro:** klasické MVC s controllers, models, views
- **Co dostaneš:** `Controllers/HomeController.cs`, `Models/`, `Views/Home/Index.cshtml`, routing v `Program.cs`

### Blazor Web App
- **Použij pro:** SPA v C# (alternativa k Reactu)
- **Co dostaneš:** Razor komponenty (`.razor`), state management v C#

### Class Library
- **Použij pro:** knihovnu, kterou pak referenceš z jiných projektů (ne na typický exam case)

---

## 🛠️ Visual Studio Scaffolding (POWER MOVE)

VS umí vygenerovat **kompletní funkční Razor Pages nebo Controllery** z **modelu + DbContext**. Ušetří 30 minut psaní.

### Jak postupovat (CRUD z modelu)

1. **Vytvořit Model:** `Models/Ukol.cs`
   ```csharp
   public class Ukol
   {
       public int Id { get; set; }
       public string Text { get; set; } = "";
       public bool Hotovo { get; set; }
   }
   ```

2. **Build solution** (Ctrl+Shift+B) — jinak scaffolding model neuvidí

3. **Add → New Scaffolded Item** (pravým tlačítkem na složku Pages nebo Controllers)
   - **Razor Pages using Entity Framework (CRUD)** — pro Razor Pages projekt
   - **MVC Controller with views, using Entity Framework** — pro MVC projekt
   - **API Controller with actions, using Entity Framework** — pro Web API

4. **Vyplň dialog:**
   - Model class: `Ukol`
   - DbContext class: klikni **+** vedle pole → vygeneruje `AppDbContext`
   - Generuje: Index, Create, Edit, Delete, Details — všechny s connection na DB

5. **Po scaffoldingu VS:**
   - Vytvoří `Data/AppDbContext.cs`
   - Přidá Razor Pages do `Pages/Ukols/Index.cshtml`, `Create.cshtml`, `Edit.cshtml`, `Delete.cshtml`, `Details.cshtml`
   - Zaregistruje `DbContext` do `Program.cs`
   - Přidá NuGet packages (EF Core, EF Tools)

6. **Vytvoř migraci a aplikuj:**
   - **Package Manager Console** (View → Other Windows → Package Manager Console)
   - `Add-Migration Init`
   - `Update-Database`

**Tohle ti za 5 minut udělá funkční CRUD aplikaci.** Pak už jen kosmetické úpravy.

### Scaffolding Identity (login/register)
- **Add → New Scaffolded Item → Identity**
- Vygeneruje login, register, logout, change password — kompletně
- Vyžaduje ASP.NET Core Identity NuGet

---

## 📦 NuGet packages — co kdy stáhnout

V VS: **Tools → NuGet Package Manager → Manage NuGet Packages for Solution**

| Téma / situace | Balíček |
|---|---|
| EF Core (DB ORM v .NET) | `Microsoft.EntityFrameworkCore` + `Microsoft.EntityFrameworkCore.Sqlite` + `Microsoft.EntityFrameworkCore.Tools` |
| EF Core scaffolding from DB | `Microsoft.EntityFrameworkCore.Design` |
| JSON serializace (moderní) | (vestavěné: `System.Text.Json`) |
| JSON serializace (legacy) | `Newtonsoft.Json` |
| HTTP requesty | (vestavěné: `System.Net.Http.HttpClient`) |
| ASP.NET Identity | `Microsoft.AspNetCore.Identity.EntityFrameworkCore` |
| Auth JWT | `Microsoft.AspNetCore.Authentication.JwtBearer` |
| Logging | (vestavěné: `Microsoft.Extensions.Logging`) |
| Testing | `xUnit` + `Moq` |

---

## ⌨️ dotnet CLI (terminál)

```bash
# Nový projekt
dotnet new console -o nazev
dotnet new webapp -o nazev          # Razor Pages
dotnet new mvc -o nazev             # MVC
dotnet new webapi -o nazev          # Web API
dotnet new blazorwasm -o nazev      # Blazor WebAssembly

# Spuštění
dotnet run
dotnet build
dotnet watch run                    # auto-restart při změně

# NuGet
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools

# EF Core migrace
dotnet ef migrations add NazevMigrace
dotnet ef database update
dotnet ef migrations remove
```

Spustit terminál v VS: **View → Terminal** (Ctrl+`)

---

## ⌨️ Visual Studio shortcuts

### Navigace
- **Ctrl+T** — Go to All (rychlé hledání souboru, třídy, metody)
- **Ctrl+Shift+F** — Find in Files (globální search)
- **F12** — Go to definition
- **Shift+F12** — Find all references
- **Ctrl+G** — Go to line
- **Ctrl+;** — search v Solution Exploreru

### Editing
- **Ctrl+Space** — IntelliSense autocomplete
- **Ctrl+.** — Quick Actions (klíčová zkratka!)
- **Ctrl+K, D** — format document
- **Ctrl+K, C** — comment selection
- **Ctrl+K, U** — uncomment
- **Ctrl+R, R** — rename symbol
- **Alt+↑/↓** — move line up/down
- **Ctrl+D** — duplicate line

### Build & Debug
- **Ctrl+Shift+B** — build solution
- **F5** — start debug
- **Ctrl+F5** — start without debug
- **F10** — step over
- **F11** — step into
- **F9** — toggle breakpoint

### Window
- **Ctrl+\\, E** — Error List
- **Ctrl+\\, T** — Task List
- **Ctrl+`** — Terminal (integrated)

---

## 📝 Code snippety v VS (napiš zkratku + Tab Tab)

### C#
- `prop` → `public int Property { get; set; }`
- `propfull` → property s backing fieldem
- `ctor` → konstruktor (s parametry tříd fieldů)
- `cw` → `Console.WriteLine();`
- `for` → for loop
- `forr` → reversed for
- `foreach` → foreach loop
- `if` → if blok
- `try` → try-catch
- `tryf` → try-finally
- `class` → class definice
- `interface` → interface definice
- `enum` → enum definice
- `switch` → switch blok
- `using` → using statement (disposable)
- `lock` → lock blok

### Razor (.cshtml)
- `@page` → page directive (Razor Pages)
- `@model` → model directive
- `@inject` → DI inject

---

## 🎯 Per téma DAT — konkrétní postup

### DAT 1 (HTML5 sémantika)
- **VS Template:** žádný — File → New Item → HTML page
- **Workflow:** napsat `<header><nav><main><article><aside><footer>`
- **Pomoc:** **Zeal F1 → HTML** docset (hledej `article`, `figure`, `form`)
- **Klíč:** `<form>` s `<fieldset><legend><label for="id"><input id="id">`

### DAT 2 (Bootstrap)
- **VS Template:** ASP.NET Core Web App **má Bootstrap už builtin** v `wwwroot/lib/bootstrap/`
- **Nebo standalone HTML:** přidej CDN do `<head>` (⚠️ bez internetu CDN nejde — použij lokální Bootstrap z `wwwroot/lib/`)
- **Klíč:** `container`, `row`, `col-md-6`, `card`, `card-body`, `btn btn-primary`, `mb-3`, `g-3`, `navbar-expand-lg`
- **Pomoc:** **Zeal F1 → Bootstrap 5** (kompletní reference komponent a utility tříd)

### DAT 3-5 (Flexbox, Grid, Pozicování)
- **VS Template:** standalone HTML+CSS file
- **Klíč Flex:** `display: flex; gap; flex-direction; justify-content; align-items`
- **Klíč Grid:** `display: grid; grid-template-columns; grid-template-areas`
- **Klíč Position:** `relative` (kotva), `absolute`, `fixed`, `sticky`
- **Pomoc:** **Zeal F1 → CSS** (hledej `flex`, `grid`, `position`, `z-index`)

### DAT 6 (CSS animace)
- **Klíč:** `@keyframes`, `animation: name duration timing iteration`, `transition`
- **Past:** `@media (prefers-reduced-motion: reduce) { ... }` — accessibility

### DAT 7-10 (C# základy, OOP, podprogramy)
- **VS Template:** **Console App** (.NET 9)
- **VS Power:** napiš `class User` → Ctrl+. → **"Generate constructor"** automaticky s parametry fieldů
- **VS Power:** napiš `class User : IComparable` → Ctrl+. → **"Implement interface"** vygeneruje signatury
- **Snippety:** `prop` + Tab Tab pro property
- **Pomoc:** `_docs/csharp/csharp-docs.pdf` (Ctrl+F pro hledání), nebo Zeal — bohužel nemá C# docset, takže PDF je primární

### DAT 11 (Algoritmy)
- **VS Template:** Console App
- **Workflow:** napiš signaturu funkce (`static int Najdi(int[] pole, int hodnota)`), pak tělo
- **Pomoc:** napiš logiku česky jako komentář, pak implementuj

### DAT 12 (Souborový systém + streamy)
- **VS Template:** Console App + `using System.IO;`
- **Klíčové API (IntelliSense ti pomůže — napiš `File.` a uvidíš metody):**
  ```csharp
  File.ReadAllText(path)
  File.WriteAllText(path, text)
  File.ReadAllLines(path)
  File.WriteAllLines(path, lines)
  File.ReadAllBytes(path)
  File.Exists(path)
  Directory.GetFiles(path, "*", SearchOption.AllDirectories)
  Directory.CreateDirectory(path)
  Path.Combine(a, b, c)
  using StreamReader sr = new StreamReader(path);
  ```
- **Pomoc:** `_docs/csharp/csharp-docs.pdf` → Ctrl+F → `File.ReadAllText` / `StreamReader`

### DAT 13 (Async/parallel)
- **VS Template:** Console App
- **Klíč:** `using System.Diagnostics;` pro `Stopwatch`
- **Klíč:** `using System.Threading.Tasks;` (často auto-imported)
- **Snippet:** napiš `async` → IntelliSense ti nabídne async metody
- **Past:** **closure v `for` cyklu** — vždy `int kopie = i;` před `Task.Run`
- **Pomoc:** `_docs/csharp/csharp-docs.pdf` → `async`, `Task.Run`, `Parallel.For`

### DAT 14 (Návrhové vzory v C#)
- **VS Template:** Console App
- **Klíč:** vyber 1 vzor (Singleton, Factory, Observer), implementuj do detailu
- **Pomoc:** `_docs/csharp/csharp-docs.pdf` → "design patterns" (má sekci)

### DAT 15 (ER diagram)
- **Tool:** nepotřebuje VS — papír / Word / lokální editor s Mermaid plugin
- **Klíč:** entity, atributy, vztahy (1:1, 1:N, M:N), kardinalita
- **M:N** se realizuje **vazební tabulkou**

### DAT 16 (SQL SELECT)
- **Tool:** **SSMS** (SQL Server Management Studio) NEBO **DB Browser for SQLite** NEBO **Azure Data Studio** (vše nainstalované na školním PC)
- **Workflow:** otevři `.db` ze zadání, napiš query, F5 spustí
- **Klíč pořadí:**
  ```sql
  SELECT sloupce
  FROM tabulka a
  [JOIN druha b ON a.id = b.a_id]
  WHERE filter_radku
  GROUP BY sloupce
  HAVING filter_skupin
  ORDER BY sloupce [DESC]
  LIMIT n;
  ```
- **Agregace:** `COUNT(*)`, `SUM(x)`, `AVG(x)`, `MIN`, `MAX`
- **Pomoc:** **Zeal F1 → SQLite** docset (syntax SQL funkcí, JOIN typy, agregace)

### DAT 17 (DDL + DML)
- **DDL:** `CREATE TABLE`, `ALTER TABLE`, `DROP TABLE`, `TRUNCATE`
- **DML:** `INSERT INTO`, `UPDATE`, `DELETE`
- **Past:** TRUNCATE = DDL (struktura), ne DML
- **Constraints:**
  ```sql
  CREATE TABLE produkty (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      nazev TEXT NOT NULL UNIQUE,
      cena REAL CHECK (cena > 0) DEFAULT 0,
      kategorie_id INTEGER,
      FOREIGN KEY (kategorie_id) REFERENCES kategorie(id) ON DELETE CASCADE
  );
  ```

### DAT 18-19 (Datové typy, kódování)
- Převody binární↔dec↔hex
- ASCII, UTF-8 vs ASCII

### DAT 20 (Next.js — Server vs Client Components, dynamic routes)

⚠️ **Zeal NEMÁ Next.js docset.** Použij `_docs/nextjs.docs/docs/` (po `docs.ps1`) NEBO opiš ty šablony rovnou odtud:

**Setup:**
```bash
npx create-next-app@latest nazev --typescript --app --no-tailwind --no-eslint --src-dir=false --import-alias="@/*"
```
⚠️ Bez internetu `npx create-next-app` nepůjde — musíš mít existující projekt.

**File-based routing:**
- `app/page.tsx` = `/`
- `app/about/page.tsx` = `/about`
- `app/blog/[slug]/page.tsx` = `/blog/:slug` (dynamic)
- `app/layout.tsx` = root layout (`<html>`, `<body>`, sdílený Navbar atd.)

**Server Component (default, žádný `"use client"`):**
```tsx
export default function Home() {
    return <h1>Domů</h1>;
}
```

**Server Component s async data fetch:**
```tsx
export default async function Page() {
    const data = await fetch("https://...").then(r => r.json());
    return <div>{data.name}</div>;
}
```

**Dynamic Route v Next 15+ (`params` je Promise — klíčový pattern):**
```tsx
export default async function Page({
    params,
}: {
    params: Promise<{ slug: string }>;
}) {
    const { slug } = await params;
    return <h1>Článek: {slug}</h1>;
}
```

**Client Component (s `"use client"`, hooky, eventy):**
```tsx
"use client";
import { useState, useEffect } from "react";

export default function Clock() {
    const [time, setTime] = useState<string | null>(null);

    useEffect(() => {
        const id = setInterval(() => setTime(new Date().toLocaleTimeString()), 1000);
        return () => clearInterval(id);
    }, []);

    return <p>{time ?? "Načítám..."}</p>;
}
```

**Link (Next.js):**
```tsx
import Link from "next/link";

<Link href="/about">O aplikaci</Link>
<Link href={`/blog/${slug}`}>{title}</Link>
```

⚠️ V Next: `<Link href="...">` (ne `to=` jako v React Router).

**Layout s navbar:**
```tsx
import Navbar from "@/components/Navbar";

export default function RootLayout({ children }: { children: React.ReactNode }) {
    return (
        <html lang="cs">
            <body>
                <Navbar />
                {children}
            </body>
        </html>
    );
}
```

**Pamatuj:**
- Server Components defaultně, `"use client"` jen pro interaktivitu
- Next 15: `params: Promise<...>` + `await params`
- File `[slug]` (hranaté závorky) = dynamic segment
- `next/link` ne `react-router-dom`

---

### DAT 20-old (REST API klient — pokud zadání chce JS/C# klient)
- **VS Template:** Console App
- **Klíč:**
  ```csharp
  using HttpClient client = new();
  string json = await client.GetStringAsync("https://api.example.com/users");
  // Pro typed:
  var users = await client.GetFromJsonAsync<List<User>>("https://...");
  ```
- **Pomoc:** `using System.Net.Http.Json;` pro `GetFromJsonAsync`
- **Doc:** `_docs/csharp/csharp-docs.pdf` → `HttpClient`, `JsonSerializer`. ⚠️ Bez internetu nemůžeš testovat real API — komise typicky poskytne mock endpoint nebo lokální server.

### DAT 21 (ORM v .NET = Entity Framework Core)
- **⚠️ U zkoušky Prchal používá Prisma 7 (TypeScript), NE EF Core.** Ale pokud zadání umožní EF Core, je to **MNOHEM rychlejší přes VS scaffolding.**
- **Prisma 7 (TypeScript) workflow:**
  ```bash
  npm init -y
  npm i prisma @prisma/client @prisma/adapter-better-sqlite3 better-sqlite3 tsx dotenv
  npx prisma init --datasource-provider sqlite
  # edit prisma/schema.prisma
  npx prisma migrate dev --name init
  ```
- **⚠️ Bez internetu `npm install` nepůjde** — balíčky musí být v `node_modules/` (předem stažené v zadání) nebo lokálním npm cache. Zeptej se učitele.
- **EF Core alternativa (pokud zadání povoluje):**
  1. Console App / Web App
  2. `dotnet add package Microsoft.EntityFrameworkCore.Sqlite Microsoft.EntityFrameworkCore.Tools`
  3. Vytvořit Model class + DbContext class
  4. `dotnet ef migrations add Init`
  5. `dotnet ef database update`
- **Doc:** `_docs/prisma.docs/content/` (markdown), `_docs/asp.net/aspnetcore-docs.pdf` → "Entity Framework Core"

### DAT 22 (React komponenty + props)
- **VS Template:** **ASP.NET Core React** (kombinovaný projekt) — VS umí.
- **Standalone Vite (lepší):**
  ```bash
  npm create vite@latest nazev -- --template react-ts
  cd nazev && npm install && npm run dev
  ```
- **⚠️ Bez internetu `npm install` selže** — balíčky musí být v lokálním npm cache nebo předem stažené `node_modules/` (často zadání obsahuje připravený projekt s `node_modules`).
- **Workflow:**
  1. Smaž boilerplate v `App.tsx`
  2. Vytvoř `types.ts` s `type Ukol = { id, text, hotovo }`
  3. `App.tsx` má `useState`, vyrobi handlery (pridat/smazat/prepnout)
  4. Dětí v `src/components/` — typed props
- **VS Code:** otevři ve VS Code (lehčí pro JS/TS), VS Studio má taky JS/TS support
- **Doc:** **Zeal F1 → React**, `_docs/react.dev/src/content/learn/`, `_docs/react.cheatsheet/` (TS+React zlato)

### DAT 23 (React hooks)
- **Klíčové:**
  ```tsx
  // useState
  const [x, setX] = useState(0);
  
  // useEffect — side effects
  useEffect(() => {
      fetchData();
      return () => cleanup();
  }, [deps]);
  
  // useRef — reference na DOM
  const ref = useRef<HTMLInputElement>(null);
  useEffect(() => { ref.current?.focus(); }, []);
  
  // useId — unique ID pro a11y
  const id = useId();
  <label htmlFor={id}>Email</label><input id={id} />
  ```
- **Doc:** `_docs/react.dev/src/content/reference/react/` — markdown soubory `useState.md`, `useEffect.md`, `useRef.md`, `useId.md`. Nebo **Zeal F1 → React**.

### DAT 24 (React Router)
- **Install:** `npm i react-router-dom`
- **Setup main.tsx:**
  ```tsx
  import { BrowserRouter } from "react-router-dom";
  createRoot(...).render(<BrowserRouter><App /></BrowserRouter>);
  ```
- **App.tsx:**
  ```tsx
  import { Routes, Route, Link, useParams, useNavigate } from "react-router-dom";
  <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/users/:id" element={<UserDetail />} />
  </Routes>
  // V UserDetail:
  const { id } = useParams();
  const nav = useNavigate();
  ```

### DAT 25 (useContext + useReducer)
- **useContext:**
  ```tsx
  const Ctx = createContext<{...}>({...});
  <Ctx.Provider value={...}><App /></Ctx.Provider>
  const ctx = useContext(Ctx);
  ```
- **useReducer:**
  ```tsx
  type Action = { type: 'ADD'; payload: Ukol } | { type: 'DELETE'; id: number };
  const reducer = (state, action) => { switch (action.type) {...} };
  const [state, dispatch] = useReducer(reducer, initial);
  dispatch({ type: 'ADD', payload: novy });
  ```

---

## ⏰ Časový management (30 min DAT praktiky)

| Min | Co dělat |
|---|---|
| 0–3 | **Přečíst zadání 2× pomalu**, vypsat keywordy, představit si finální výsledek |
| 3–6 | **Scaffold** — File → New Project (VS) NEBO `npm create vite` / `dotnet new`. **Spusť template** (F5) — ověř že běží! |
| 6–10 | **Datový model** (Model.cs / types.ts) + kostra hlavního souboru |
| 10–25 | **Implementace iterativně** — napiš 10 řádků → spusť → ověř → pokračuj |
| 25–28 | **Cleanup** — smaž zbytky boilerplate komentářů, ověř finální funkčnost |
| 28–30 | **Připrav úvod obhajoby:** 3 věty *"Vyrobil jsem X pomocí Y, klíčové části jsou Z."* |

**Pravidlo 70%:** v 20. minutě bez fungujícího jádra → priorita = **mít NĚCO funkčního, ne dokonalého**. Hello World > nedokončený ambiciózní pokus.

---

## 💀 Last resort taktiky

### "Neumím to ani spustit"
1. **File → New Project** → vyber template odpovídající zadání
2. **F5** — spusť default template (Hello World)
3. **Studuj co default template vygeneroval** — `Program.cs`, struktura
4. **Pak začni měnit/přidávat** — minimální změny od pracujícího baseline

### "Nevím syntax / API"
1. **IntelliSense Ctrl+Space** — napiš `File.` a uvidíš co `File` umí
2. **Zeal F1** — instant search v offline docsetech (HTML/CSS/JS/TS/React/Bootstrap/Node.js/SQLite)
3. **Ctrl+F v PDF** — `_docs/csharp/csharp-docs.pdf` nebo `_docs/asp.net/aspnetcore-docs.pdf`
4. **Hover na proměnnou** — VS ukáže typ + XML doc comment (pokud existuje)
5. **Quick Actions Ctrl+.** — generování standard code, add usings

### "Kompilace selhává"
1. **Error List** (Ctrl+\\, E)
2. **První error oprav jako první** — ostatní mohou být kaskáda
3. **Ctrl+.** nad červeným podtržením → 90% případů vygeneruje opravu
4. Typické problémy: chybí `using` (C#) / `import` (TS), překlep v názvu, mismatch typu

### "Mám 5 minut a nemám nic"
1. **Funkční minimum:** scaffold default template + 1 vlastní řádek navíc
2. Komise ocení **funkční Hello World s 1 vlastní úpravou** víc než **nefunkční komplikovaný pokus**
3. Při obhajobě: *"Soustředil jsem se na funkční jádro, dál bych pokračoval [popiš plán]"*

### "U obhajoby se ptají na něco co neumím"
1. *"Přesnou syntax tady nemám v paměti, ale princip je..."* — pokračuj na koncept
2. *"V mém kódu to vidíme tady..."* — ukaž v Solution Exploreru, navigace dělá dojem znalosti
3. *"Použil bych Zeal / `_docs/csharp.pdf` na konkrétní API"* — kde bys to hledal = znalost
4. **Nelži.** *"Tady si nejsem jistý"* > blábol. Komise to pozná.

---

## 🔄 Workflow jako mantra

```
1. Scaffold default template (F5 → vidíš Hello World)
2. Identify klíčový soubor (Program.cs / App.tsx / Page.cshtml.cs)
3. Napsat nejmenší krok → uložit → ověřit (F5 nebo HMR)
4. Iterovat malými krocky
5. Při zaseknutí: IntelliSense → F1 → Ctrl+. → komentář v zadání
```

**Pokud po 5 minutách jsi pořád u Hello Worldu, něco je špatně.** Restartuj template, zkus jiný.

---

**Vyrobeno:** 2026-05-21, Den 14, 4 dny před zkouškou (5/25).
