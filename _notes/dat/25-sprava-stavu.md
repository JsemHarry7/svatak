# DAT 25 — Správa stavů v Reactu (useContext + useReducer)

> **Cíl:** umět spravovat stav složitější aplikace bez prop drillingu a s komplexními update operacemi.
> **2 hlavní hooky:** `useContext` (globální state), `useReducer` (composite state s komplexními updaty).

---

## Proč existují

V malém TodoListu si vystačíš s `useState` v App + props dolů. Ale jak aplikace roste, narazíš na **dva problémy**:

### Problém 1 — Prop drilling

```
App (drží user data)
 └── Layout
     └── Sidebar
         └── UserMenu
             └── Avatar  ← jen TADY chceš user data
```

Předáváš `user` přes 4 úrovně, kde 3 z nich ho nepotřebují, jen propustí dál. To je **prop drilling** — kód špinavý, refactor bolí, props bloatí typy.

**Řešení: `useContext`** — komponenta odkudkoli v hierarchii sáhne přímo na data, bez propagace přes mezistupně.

### Problém 2 — Composite state s komplexními updaty

V TodoListu máš 3 různé operace nad `ukoly`:
```tsx
setUkoly([...ukoly, novy]);                                    // ADD
setUkoly(ukoly.filter(u => u.id !== id));                      // DELETE
setUkoly(ukoly.map(u => u.id === id ? {...u, hotovo: !u.hotovo} : u));  // TOGGLE
```

3 různé updaty rozházené po App. **Co když přibude 5 dalších** (priority, deadline, edit, archive, ...) → App.tsx exploduje.

**Řešení: `useReducer`** — všechny updaty centralizované v jedné funkci (reducer). Component jen "vystřelí akci" a reducer ví, jak na ni reagovat.

---

## `useContext` — globální state

### 3 kroky

#### 1. Vytvoř Context

```tsx
import { createContext } from "react";

type ThemeContextType = {
    theme: "light" | "dark";
    setTheme: (t: "light" | "dark") => void;
};

export const ThemeContext = createContext<ThemeContextType | null>(null);
```

`createContext(default)` vytvoří **kontext objekt**. Default hodnota (`null` nebo cokoli) se použije, pokud komponenta čte mimo Provider.

#### 2. Obal komponenty Providerem

```tsx
function App() {
    const [theme, setTheme] = useState<"light" | "dark">("light");
    
    return (
        <ThemeContext.Provider value={{ theme, setTheme }}>
            <Layout />
            <Sidebar />
            <Footer />
        </ThemeContext.Provider>
    );
}
```

`<Context.Provider value={...}>` zpřístupní `value` všem komponentám uvnitř. **Kdokoli pod Providerem** může číst `theme` a `setTheme`.

#### 3. Čti přes `useContext` kdekoli v hierarchii

```tsx
import { useContext } from "react";
import { ThemeContext } from "./ThemeContext";

function Avatar() {
    const ctx = useContext(ThemeContext);
    if (!ctx) throw new Error("Avatar must be inside ThemeProvider");
    
    return <div className={ctx.theme}>...</div>;
}
```

**Žádné props.** Avatar sáhne přímo na theme, jakkoli hluboko v hierarchii.

### Kdy NE useContext

- **Pro pár props** — overkill, props jsou OK
- **Pro stav, který mění VŠICHNI uvnitř** — každá změna re-renderuje **všechny consumery** (i ty, co tu změnu nezajímá). Pro extrémně dynamický state → state management knihovny (Redux, Zustand, Jotai)
- **Nikdy ne pro server state** — k tomu jsou specializované nástroje (React Query, SWR)

### Klasický pattern: Custom hook nad Contextem

Místo `useContext(ThemeContext)` všude, vytvoř wrapper:
```tsx
export const useTheme = () => {
    const ctx = useContext(ThemeContext);
    if (!ctx) throw new Error("useTheme must be inside ThemeProvider");
    return ctx;
};

// Použití:
const { theme, setTheme } = useTheme();
```

Komponenty pak vůbec neznají `ThemeContext` — jen volají `useTheme()`. Čistší.

---

## `useReducer` — komplexní updaty

Místo `setState(novaHodnota)` posíláš **akci** (objekt) do funkce **reduceru**, která vrátí nový state.

### Mentální model

```
User klikne "Přidat" →  dispatch({ type: 'ADD', payload: { text: 'koupit chleba' } })
                                                          │
                       ┌───────────────────────────────────┘
                       ▼
            reducer(state, action) {
                switch (action.type) {
                    case 'ADD': return [...state, novyUkol];
                    case 'DELETE': return state.filter(...);
                    case 'TOGGLE': return state.map(...);
                }
            }
                       │
                       ▼
                  nový state
                       │
                       ▼
                  re-render
```

### 4 části

#### 1. Type pro akci (TypeScript)

Discriminated union — každá akce má `type` literál + případně `payload`.

```tsx
type UkolAction =
    | { type: "ADD"; text: string }
    | { type: "DELETE"; id: number }
    | { type: "TOGGLE"; id: number };
```

#### 2. Reducer funkce (čistá funkce)

```tsx
function ukolyReducer(state: Ukol[], action: UkolAction): Ukol[] {
    switch (action.type) {
        case "ADD":
            return [...state, { id: Date.now(), text: action.text, hotovo: false }];
        case "DELETE":
            return state.filter(u => u.id !== action.id);
        case "TOGGLE":
            return state.map(u => u.id === action.id ? { ...u, hotovo: !u.hotovo } : u);
        default:
            return state;
    }
}
```

**Pravidla reduceru:**
- **Čistá funkce** — žádné side effects (fetch, console.log), žádná mutace state
- **Vrací NOVÝ state** (imutabilita jako u useState — spread/map/filter)
- **Switch nad `action.type`** — idiomaticky

#### 3. Hook v komponentě

```tsx
const [ukoly, dispatch] = useReducer(ukolyReducer, []);
```

Tuple: `[state, dispatch]`. Podobné `[state, setState]`, ale místo setteru máš `dispatch` (vystřelí akci).

#### 4. Dispatch akce

```tsx
dispatch({ type: "ADD", text: "koupit chleba" });
dispatch({ type: "DELETE", id: 5 });
dispatch({ type: "TOGGLE", id: 3 });
```

### Kdy useReducer místo useState

| useState | useReducer |
|---|---|
| Simple state (`count`, `text`) | **Composite state** (objekt, pole s víc operacemi) |
| 1-2 updaty | **3+ různých update operací** |
| Updaty na sobě nezávisí | **Update logika je složitá / spolu souvisí** |
| Logika v komponentě OK | **Chceš logiku centralizovat / testovat** |

**Rozhodovací pravidlo:** pokud máš `setUkoly([...])` na 5 různých místech s různou logikou → useReducer to vyčistí.

### Bonus: useReducer + useContext = mini Redux

Kombinace obojího = **globální state s centralizovanou update logikou**. Komponenty kdekoli můžou:
1. Číst `ukoly` přes context
2. Volat `dispatch` přes context

Bez `redux` knihovny, jen vestavěnými React hooks.

---

## Náš plán pro TodoList

Refactor:
- `useState` na `ukoly` → **`useReducer`** (3 různé updaty centralizované)
- Prop drilling (App → Home → TodoList → TodoItem) → **Context** (kdokoli sáhne na ukoly + dispatch)

### Nová architektura

```
App.tsx
 └── UkolyProvider  ← drží useReducer + poskytuje context
     ├── nav
     └── Routes
         ├── Home          (čte ukoly + dispatch přes useUkoly hook)
         ├── About         (žádná data)
         ├── UkolDetail    (čte ukoly + dispatch přes useUkoly)
         └── Statistiky    (čte ukoly přes useUkoly)
```

---

## Časté chytáky

1. **Reducer je čistá funkce** — žádný `console.log`, žádný fetch, žádná mutace
2. **Provider musí být NAD všemi consumery** (jako BrowserRouter, ale pro state)
3. **Context re-renderuje všechny consumery** při změně value — pokud je hodně dat → optimalizace (rozdělit context na user/theme/data atd.)
4. **Discriminated union** pro actions — TS pak ví, že u `type: 'DELETE'` je `payload.id: number`, ne text
5. **`as const` na string literálech** — někdy potřeba pro správné type inference

---

## Co se mě může komise ptát

**Q:** Co je useContext?
**A:** Hook pro čtení **globálního state** bez prop drillingu. Obalíš část aplikace `<Context.Provider value={...}>`, kdekoli uvnitř pak `useContext(Context)` vrátí to value.

**Q:** Co je useReducer?
**A:** Hook pro **state s komplexními updaty**. Místo setteru máš `dispatch(action)`, action putuje do **reducer funkce** která vrací nový state.

**Q:** Kdy useReducer místo useState?
**A:** Když máš 3+ různých update operací nad stejným state, nebo když update logika je složitější než jednořádkový setter. Centralizace + testovatelnost.

**Q:** Co je reducer?
**A:** **Čistá funkce** `(state, action) => newState`. Žádné side effects, vrací nový state imutabilně.

**Q:** Proč useContext místo props?
**A:** Vyhne se **prop drillingu** přes komponenty, které data nepotřebují, jen je propouští dál.

**Q:** Kombinace useContext + useReducer?
**A:** "Mini Redux" — globální state přes context + centralizované updaty přes reducer. Bez externí knihovny.
