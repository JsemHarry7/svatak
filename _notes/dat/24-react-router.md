# DAT 24 — React Router

> **Cíl:** umět postavit a obhájit multi-page React aplikaci s navigací.
> **Co to je:** knihovna pro **client-side routing** v SPA aplikacích. Mění URL bez page reloadu, drží stav, podporuje back button.
> **Balíček:** `react-router-dom` (verze 6+)

---

## Proč Router existuje

**SPA = jedna HTML stránka.** Bez routeru bys měl všechen obsah v jedné komponentě (`if (zobrazit === "about") ...`). Router řeší:

1. **URL reflektuje stav** — `/about` zobrazí About, `/ukol/5` zobrazí detail úkolu 5
2. **Sdílení URL funguje** — pošleš kamarádovi `/ukol/5`, otevře přímo detail
3. **Back button funguje** — Router používá HTML5 History API
4. **Žádný page reload** — JS jen vymění obsah části DOMu

---

## 5 klíčových věcí

### 1. `BrowserRouter` (obal, jen 1× v aplikaci)

Patří do **`main.tsx`** (ne App.tsx), aby celá `App` měla přístup k routing kontextu.

```tsx
// main.tsx
import { BrowserRouter } from "react-router-dom";

createRoot(...).render(
    <StrictMode>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </StrictMode>
);
```

**Proč v main.tsx ne App.tsx:** uvnitř App.tsx už chceš používat hooky jako `useNavigate`. Aby fungovaly, musí být **uvnitř** BrowserRouter. Když ho dáš do App.tsx, ten samotný App nemá přístup ke kontextu.

**Alternativy:**
- `HashRouter` — URL s `#` (`/#/about`). Použít když nemáš server kontrolu (např. GitHub Pages).
- `MemoryRouter` — pro testy, žádné URL v browser.

### 2. `<Routes>` + `<Route>` (definice cest)

```tsx
<Routes>
    <Route path="/" element={<Home />} />
    <Route path="/about" element={<About />} />
    <Route path="/ukol/:id" element={<UkolDetail />} />
    <Route path="*" element={<NotFound />} />
</Routes>
```

**Pravidla:**
- `<Routes>` = wrapper, drží seznam `<Route>` dětí
- `<Route path="..." element={...} />` = pár URL → JSX
- **`element`** = libovolný JSX, většinou jedna page komponenta
- **`:id`** = dynamic parametr (např. `/ukol/5` matchuje s `id="5"`)
- **`*`** = wildcard, fallback (404)
- Router 6 matchuje **podle specificity, ne pořadí v JSX**

### 3. `<Link>` (klikání bez reloadu)

```tsx
import { Link } from "react-router-dom";

<Link to="/about">O aplikaci</Link>
<Link to={`/ukol/${ukol.id}`}>{ukol.text}</Link>
```

**Generuje to `<a href="...">`**, ale klik je zachycen JS → URL se změní bez HTTP requestu.

**Rozdíl `<Link>` vs `<a>`:**
| `<a href="/about">` | `<Link to="/about">` |
|---|---|
| Plný page reload | Client-side navigate |
| Ztratíš state | State zachován |
| Pro externí URL OK | Pro vnitřní URL **vždy Link** |

**Pravidlo:** uvnitř SPA vždy `Link`. `<a>` jen pro externí URL (`https://google.com`).

### 4. `useParams` (číst `:id` z URL)

```tsx
import { useParams } from "react-router-dom";

function UkolDetail() {
    const { id } = useParams();   // id = "5" (vždy STRING!)
    
    const ukol = ukoly.find(u => u.id === Number(id));   // konvertuj na číslo!
    // ...
}
```

**Klíčové:** `useParams` vrací **objekt se stringy**. Pokud porovnáváš s number (např. `u.id: number`), MUSÍŠ konvertovat přes `Number(id)` nebo `parseInt(id, 10)`.

**Past:** `"5" === 5` je `false` (různé typy). `"5" === "5"` je `true`. Vždy zkontroluj typy.

### 5. `useNavigate` (programatická navigace)

```tsx
import { useNavigate } from "react-router-dom";

function Komponenta() {
    const nav = useNavigate();
    
    const handleSubmit = async () => {
        await ulozFormulare();
        nav("/seznam");        // → URL: /seznam
    };
    
    const back = () => nav(-1);   // back v browser history
    
    return (
        <>
            <button onClick={handleSubmit}>Uložit</button>
            <button onClick={back}>Zpět</button>
        </>
    );
}
```

**Kdy `useNavigate` místo `<Link>`:**
- Po akci (uložit form, smazat položku, login) chceš navigovat **z handleru**, ne klikem
- `nav(-1)` simuluje browser back tlačítko
- Conditional navigation (`if (success) nav("/done")`)

---

## Náš multi-page TodoList — co kde je

```
src/
├── main.tsx              ← <BrowserRouter><App /></BrowserRouter>
├── App.tsx               ← state ukoly + <Routes>
├── types.ts              ← type Ukol
├── pages/
│   ├── Home.tsx          ← TodoForm + TodoList, dostává ukoly přes props
│   ├── About.tsx         ← statická info stránka
│   ├── NotFound.tsx      ← 404 stránka s <Link to="/">
│   └── UkolDetail.tsx    ← useParams + useNavigate, čte ukoly přes props
└── components/
    ├── TodoForm.tsx      ← controlled input + useId + useRef
    ├── TodoList.tsx      ← .map(ukol => <TodoItem />)
    └── TodoItem.tsx      ← <Link to={`/ukol/${ukol.id}`}>{text}</Link>
```

**Tok dat:**
- `App.tsx` drží `ukoly` v `useState` (single source of truth)
- Předává je přes props do `<Home>` i `<UkolDetail>` (oba potřebují stejná data)
- Změny: callbacky `pridatUkol`, `smazatUkol`, `prepnoutHotovo` v App, propagují se dolů

---

## Časté chytáky

1. **`useParams` vrací stringy**, ne čísla. `Number(id)` před porovnáním.
2. **`<Link>` ne `<a>`** uvnitř SPA.
3. **`BrowserRouter` musí být nad `App`**, jinak hooky uvnitř App nefungují.
4. **Wildcard `*` jako poslední** — chytá vše neuvedené.
5. **Po refresh některé URL nefungují** (např. `/about`) v produkci, pokud server nepřesměrovává na `index.html`. To je problém deploy konfigurace, ne Routeru.

---

## Co se mě může komise ptát

**Q:** K čemu je React Router?
**A:** Client-side routing v SPA — URL reflektuje stav, navigace bez page reloadu, sdílení URL funguje.

**Q:** Rozdíl `<Link>` × `<a>`?
**A:** `<Link>` neperef rešuje stránku, `<a>` ano. Uvnitř SPA vždy `<Link>`.

**Q:** Co je `BrowserRouter`?
**A:** Obal aplikace, který poskytuje routing kontext (URL, history) pro hooky a komponenty uvnitř.

**Q:** Jak vytáhneš `:id` z URL?
**A:** Hook `useParams()`. Vrací objekt se stringy. `const { id } = useParams()`.

**Q:** Kdy `useNavigate` místo `<Link>`?
**A:** Když potřebuju navigovat **z kódu** (po akci, async), ne klikem uživatele. Příklad: po uložení form → `nav("/seznam")`.

---

## Drobnosti k zapamatování

- **`react-router-dom`** ≠ `react-router` (dom je pro web, sám react-router je core)
- Verze 6+ → API jako popsáno výše. Verze 5 měla `<Switch>` místo `<Routes>` (legacy).
- **NavLink** = `<Link>` s automatickým `active` třídou (užitečné pro navbary, ale není v zadání DAT 24)
