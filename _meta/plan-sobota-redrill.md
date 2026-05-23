# 📅 Sobota 5/23 — DAT full redrill plán

> **Cíl:** refresh paměti všech 25 DAT témat za 1 den. **Ne se to učit, jen oprašovat.**
> **Rozdělené do 5 bloků** podle technologického proudu.

---

## Pravidla redrillu (per téma ~15 min)

Každé téma stejný rytmus:

1. **3 min — flashcards** (`_notes/cards/dat-NN-*.md`) — projdi Q/A, zaškrtni kde váháš
2. **5 min — zápisky** (`_notes/dat/NN-*.md`) — projdi rychle, fokus na sekci **"Časté chytáky"** + **"Q&A pro komisi"**
3. **5 min — aktivní recall** — bez koukání:
   - Co je to (1 věta)
   - 3 klíčové pojmy
   - 1 code snippet z hlavy
4. **2 min — quick verify** — porovnej co jsi řekl s notes, oprav

**NEUČ SE nové věci.** Pokud něco neumíš ani teď, **přeskoč** a vrať se k tomu na neděli.

---

## Časové bloky

| Blok | Témata | Čas | Plánovaná hodina |
|---|---|---|---|
| **Blok 1 — Web základ** | DAT 1, 2, 3, 4, 5, 6 (HTML, Bootstrap, Flexbox, Grid, Pozicování, Animace) | 1h 30min | 8:30–10:00 |
| ☕ **Pauza** | | 15 min | 10:00–10:15 |
| **Blok 2 — C# základy** | DAT 7, 8, 9, 10, 11 (Tabulky, Datové typy, Spojové struktury, Podprogramy, Kolekce) | 1h 15min | 10:15–11:30 |
| ☕ **Pauza + jídlo** | | 45 min | 11:30–12:15 |
| **Blok 3 — C# pokročilý** | DAT 12, 13, 14 (Soubory, Async, Git) | 45 min | 12:15–13:00 |
| **Blok 4 — DB + API** | DAT 15, 16, 17, 18, 19, 20, 21 (ER, SQL, REST, Razor, Tag Helpers, Next, ORM) | 1h 45min | 13:00–14:45 |
| ☕ **Pauza** | | 30 min | 14:45–15:15 |
| **Blok 5 — React** | DAT 22, 23, 24, 25 (Komponenty, Hooks, Router, Context/Reducer) | 1h | 15:15–16:15 |
| 🔥 **Hot chytáky drill** | Nejdůležitější chytáky z celého DAT, 1 hodina koncentrace | 1h | 16:15–17:15 |
| **End** | | | **17:15** |

**Celkem aktivního času:** ~6h 15min, s pauzami ~8h 45min.

---

## Blok 1 — Web základ (8:30–10:00)

### DAT 1 — HTML5 sémantika (15 min)
- Sémantické tagy: `<header><nav><main><article><section><aside><footer>`
- `<form>` s `<fieldset><legend><label for><input id>`
- `<figure>` + `<figcaption>`
- **Recall:** napiš strukturu blogu s navigací

### DAT 2 — Bootstrap (15 min)
- Klíčové třídy: `container`, `row`, `col-md-6`, `card`/`card-body`, `btn`, `mb-3`, `g-3`
- `navbar-expand-lg`, `bg-dark text-light`
- **Recall:** napiš navbar + 3 cards v gridu

### DAT 3 — Flexbox (15 min)
- `display: flex`, `flex-direction`, `gap`, `justify-content`, `align-items`
- `flex: 1`, `flex-basis`, `flex-wrap`
- **Recall:** napiš horizontální navbar s rovnoměrně rozloženými prvky

### DAT 4 — CSS Grid (15 min)
- `grid-template-columns`, `grid-template-areas`, `grid-area`
- `1fr`, `auto`, `repeat(3, 1fr)`, `gap`
- **Recall:** napiš "holy grail" layout (header, aside, main, footer)

### DAT 5 — Pozicování + z-index (15 min)
- `relative` (kotva), `absolute`, `fixed`, `sticky`
- `z-index` + stacking context triggers (transform, opacity)
- **Recall:** sticky header + fixed modal s backdrop

### DAT 6 — CSS animace (15 min)
- `@keyframes`, `animation: name dur timing iter`, `transition`
- `@media (prefers-reduced-motion: reduce)`
- **Recall:** spinner + hover scale na tlačítku

---

## Blok 2 — C# základy (10:15–11:30)

### DAT 7 — Tabulky (HTML, 15 min)
- `<table>`, `<thead>`, `<tbody>`, `<tr>`, `<th>`, `<td>`, `<caption>`
- `colspan`, `rowspan`, `scope`

### DAT 8 — Datové typy a pole (C#, 15 min)
- Primitivní (int, long, double, decimal, bool, char, string)
- Pole: `int[] x = new int[10]`, `int[] x = { 1, 2, 3 }`
- 2D: `int[,] m = new int[3, 4]`
- **Past:** float `==` ZAKÁZÁNO

### DAT 9 — Spojové struktury, stromy (15 min)
- LinkedList (singly, doubly)
- Binární strom, BST
- O(log n) vyhledávání v vyváženém

### DAT 10 — Podprogramy + lambda (15 min)
- Metoda × funkce (return type, parametry)
- `ref`, `out`, `in` parametry
- Lambda: `x => x * 2`, `(a, b) => a + b`
- `Func<int, int>`, `Action<int>`

### DAT 11 — Kolekce (15 min)
- `List<T>`, `Dictionary<K, V>`, `HashSet<T>`, `Queue<T>`, `Stack<T>`
- LINQ: `.Where()`, `.Select()`, `.OrderBy()`, `.GroupBy()`

---

## Blok 3 — C# pokročilý (12:15–13:00)

### DAT 12 — Souborový systém (15 min)
- `File.ReadAllText`, `File.WriteAllText`, `File.ReadAllLines`, `File.ReadAllBytes`
- `Directory.GetFiles(path, "*", SearchOption.AllDirectories)`
- `Path.Combine`, `using StreamReader`
- **Past:** EOL `\r\n` vs `\n`, encoding UTF-8

### DAT 13 — Async/paralelní (15 min)
- CPU-bound → `Task.Run` / `Parallel.For`
- I/O-bound → `async`/`await`
- `Task.WhenAll`, `Stopwatch`, `Environment.ProcessorCount`
- **Past:** closure v `for` cyklu — `int kopie = i;`

### DAT 14 — Git + GitHub (15 min)
- `init`, `add`, `commit`, `log`, `status`, `diff`
- `branch`, `switch`, `merge`, `merge --abort`
- `.gitignore` (na trackované soubory nepůsobí)
- Pull request flow

---

## Blok 4 — DB + API + Web (13:00–14:45)

### DAT 15 — ER model (15 min)
- Entity, atributy, vztahy (1:1, 1:N, M:N)
- **M:N přes vazební tabulku**
- Surrogate keys best practice (interní ID, ne email)

### DAT 16 — SQL výběr (15 min)
- `SELECT ... FROM ... JOIN ... ON ... WHERE ... GROUP BY ... HAVING ... ORDER BY ... LIMIT`
- Agregace: `COUNT(*)`, `SUM`, `AVG`, `MIN`, `MAX`
- **Past:** HAVING pro filter skupin, WHERE pro filter řádků
- **Past:** SQLite concat `||`

### DAT 17 — REST API v ASP.NET (15 min)
- `[ApiController]`, `[Route]`, `[HttpGet]/[HttpPost]/[HttpPut]/[HttpDelete]`
- `ActionResult<T>`, `Ok()`, `NotFound()`, `CreatedAtAction()`, `NoContent()`
- DI DbContext
- **❌ Klasická chyba:** chybí `_db.SaveChanges()` po `Add()/Remove()/Update()`

### DAT 18 — Razor Pages (15 min)
- `Pages/X.cshtml` + `X.cshtml.cs` (PageModel)
- Handlery: `OnGet`, `OnPost`, `OnPostNazev` (s `asp-page-handler`)
- `[BindProperty]`, `[BindProperty(SupportsGet=true)]`
- PRG pattern (Post-Redirect-Get)

### DAT 19 — Tag Helpers + formuláře (15 min)
- `asp-for` (binding), `asp-items` (select options), `asp-validation-for`
- `asp-page-handler` (POST handler), `asp-route-X` (URL params)
- `[Required]`, `[EmailAddress]`, `[StringLength]`
- `data-val-*` = klientská validace

### DAT 20 — Next.js (15 min)
- Framework nad Reactem
- App Router: `app/page.tsx`, `app/[id]/page.tsx`, `layout.tsx`
- Rendering: SSG / SSR / ISR / CSR
- **Server Components default** (bez hooks, můžou async)
- `"use client"` direktiva pro Client Components

### DAT 21 — Prisma ORM (15 min)
- `schema.prisma` (datasource + generator + models)
- `npx prisma migrate dev`, `npx prisma generate`
- CRUD: `findMany`, `findUnique`, `create`, `update`, `delete`
- `include` (relace), `select` (jen některá pole)
- **Past:** Prisma 7 generator `"prisma-client"`, driver adapter

---

## Blok 5 — React (15:15–16:15)

### DAT 22 — Komponenty (15 min)
- Komponenta = funkce + props + JSX
- Typed props (`type XxxProps`)
- Lifting state up (stav v nejbližším společném předkovi)
- **Data tečou shora dolů**, callbacky volané v dětech = info nahoru
- Imutabilita: spread / map / filter
- `key` v `.map` — stabilní ID

### DAT 23 — Hooks (15 min)
- 3 pravidla hooks (top-level, jen z komponent, stejné pořadí)
- `useState`, `useEffect`, `useRef`, `useId`
- Dep array: `[]` = mount, `[x]` = po x, bez deps = každý render
- **Cleanup:** deps change NEBO unmount
- **Race condition fetch:** AbortController v cleanup

### DAT 24 — React Router (15 min)
- `BrowserRouter` v main.tsx
- `<Routes>` + `<Route path="..." element={...} />`
- `<Link to="/...">` (vždy `/` na začátku = absolutní)
- `useParams()` (vrací stringy → `Number(id)`)
- `useNavigate()` (`nav("/")`, `nav(-1)`)

### DAT 25 — Context + Reducer (15 min)
- `createContext` + `<Context.Provider value={...}>`
- `useContext(Context)` — globální state bez prop drillingu
- `useReducer(reducer, initial)` — composite state s komplexními updaty
- Reducer = **čistá funkce** `(state, action) => newState`
- Discriminated union pro `Action` type

---

## 🔥 Hot chytáky drill (16:15–17:15)

Projet všechny klasické chytáky DAT, **3× pomalu**:

### React
1. **Data tečou SHORA DOLŮ** v Reactu — chyták klasický u DAT 22
2. **Imutabilita** — vždy spread/map/filter, NIKDY push/splice/mutace
3. **`key`** = stabilní ID, NE index pro dynamické seznamy
4. **`onClick={fn()}` vs `onClick={() => fn()}`** — první volá HNED
5. **Pravidla hooks** — top-level, jen z komponent, stejné pořadí
6. **Cleanup** — deps change NEBO unmount
7. **`useParams` vrací stringy** — `Number(id)` před `===`
8. **`<Link to="/...">`** — vždy `/` na začátku (absolutní URL)
9. **`"use client"`** v Next.js — direktiva pro Client Component (default je Server)

### C#
10. **`var` v C#** = type inference (jiné než JS!). V JS `var` = legacy, používej `const`/`let`.
11. **Closure v `for` cyklu** — `int kopie = i;` před `Task.Run`
12. **`async void`** = jen pro event handlery. Jinak vždy `async Task`.

### SQL
13. **HAVING** pro filter skupin, **WHERE** pro filter řádků
14. **TRUNCATE = DDL**, ne DML
15. **SQLite concat `||`**, ne `+`
16. **M:N přes vazební tabulku** (ne přímo)

### ASP.NET / EF Core
17. **Vždy `SaveChanges()`** po `Add/Update/Remove`
18. **PRG pattern** (Post-Redirect-Get) v Razor Pages
19. **`[BindProperty]`** pro POST, **`(SupportsGet=true)`** pro GET pretty URL

### CSS
20. **Specificity tuple 4 kategorie:** inline × ID × [class+attr+pseudo-class] × [element+pseudo-element]
21. **Sticky NESMÍ mít předka s `overflow:hidden`**
22. **`transform: scale(1)`** vytvoří nový stacking context
23. **`%` u padding/margin** = vůči **WIDTH** rodiče (i pro vertikální!)

### Next.js
24. **Server Components default** v App Router — bez hooks, můžou async
25. **`<Link href="...">`** v Next (ne `to=` jako v React Router)
26. **`params` v Next 15** je Promise — `await params`

---

## Po skončení (večer 5/23)

1. **Bilance:** kde jsi pevný (✅), kde 🟡, kde ještě ❌
2. **Update progress.md** — pokud někde žluté, poznámka co drillnout znovu
3. **Klidnější neděle:** finální vyladění + ČJL doplnění + spánek brzy

---

## Pravidla pro celý den

- **Sirupisý čas, ne wattaž** — kvalita nad kvantitou. Lépe 8 témat pevně než 25 povrchně.
- **Nezatahuj se v jednom tématu** — pokud nesedí po 5 minutách, přeskoč, vrať se večer/neděli
- **Hydratuj** — voda každou hodinu, ne kafe na lačno
- **Mozkové pauzy** — každých 90 min 15-30 min pryč od PC
- **NEUČ se nic nového** — to už jsi dělal celý týden. Tohle je opakování.
- **Zakaž si Instagram/Discord** — focus mode
- **Jdi spát do 23:00** — neděle bude lehčí, ale potřebuješ rezervu

---

**Generováno:** 2026-05-22, Den 15.
