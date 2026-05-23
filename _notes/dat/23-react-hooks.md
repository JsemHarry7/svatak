# DAT 23 — React hooks (useState, useEffect, useRef, useId)

> **Cíl:** umět používat 4 základní hooky + cleanup, dep arrays, race condition.
> **Formát zkoušky:** podle minulých let typicky **doplnit useEffect (a další hooky) do existujícího starter projektu** (formulář s TODO komentáři).

---

## Co je hook

**Hook = speciální funkce začínající `use`**, která dává **funkcionálním komponentám** schopnosti tříd (stav, lifecycle, refs).

Před React 16.8 (2019) tyhle schopnosti měly **jen třídní komponenty**. Dnes je default funkční + hooks.

```tsx
import { useState, useEffect, useRef, useId } from "react";
```

## Pravidla hooks (3, často chyták)

1. **Top-level only** — ne v `if`, `for`, `while`, vnořených funkcích
2. **Jen z React komponent nebo custom hooks** (funkce začínající `use`)
3. **Stejné pořadí při každém renderu** — React identifikuje hooky pořadím, ne názvem

```tsx
// ❌ Podmíněný hook = crash
if (prihlasen) {
    const [data, setData] = useState(null);
}

// ✓ Hook vždy zavolán, podmínka jen v logice
const [data, setData] = useState(null);
if (prihlasen) { /* používej data */ }
```

**Proč:** React si interně vede seznam hooks per komponenta. Identifikuje je **podle pořadí volání**. Podmíněný hook = posunutý index = popletený stav.

---

## `useState` — lokální stav

```tsx
const [pocet, setPocet] = useState<number>(0);

setPocet(pocet + 1);           // ← klasická
setPocet(prev => prev + 1);    // ← funkční update (bezpečnější při více kliknutí)
```

### Typed state

```tsx
const [text, setText] = useState("");                       // odvozeno: string
const [ukoly, setUkoly] = useState<Ukol[]>([]);             // explicit pro prázdné pole
const [user, setUser] = useState<User | null>(null);        // nullable
const [filter, setFilter] = useState<"vse" | "aktivni">("vse");  // union
```

**Lazy initializer** — pokud výchozí hodnota vyžaduje výpočet:
```tsx
const [ukoly, setUkoly] = useState<Ukol[]>(() => {
    const saved = localStorage.getItem("data");
    return saved ? JSON.parse(saved) : [];
});
// Funkce se spustí JEN 1× při mount, ne při každém renderu
```

---

## `useEffect` — side effects

**Side effect = něco, co mění svět mimo komponentu:**
- Fetch z API
- `document.title = ...`
- `localStorage.setItem(...)`
- Subscribe / event listener
- Timer (`setInterval`)

### Syntax

```tsx
useEffect(() => {
    // SETUP — co udělat
    
    return () => {
        // CLEANUP — co uklidit
    };
}, [dependencies]);
```

### Dependency array — KDY se efekt spustí

| Dep array | Kdy běží |
|---|---|
| `[]` | **Jen 1× po mount** |
| `[x]` | Po mount + při změně `x` |
| `[x, y]` | Po mount + při změně x nebo y |
| neuveden | **Po každém renderu** ⚠️ (často infinite loop) |

### Cleanup function

Vrácená funkce z useEffectu. Spouští se:
1. **Před dalším spuštěním efektu** (deps change)
2. **Při unmount komponenty**

```tsx
useEffect(() => {
    const id = setInterval(() => setCas(new Date()), 1000);
    return () => clearInterval(id);   // ← bez toho timer tikne věčně
}, []);
```

**Co potřebuje cleanup:**
- `setInterval` / `setTimeout` → `clearInterval` / `clearTimeout`
- `window.addEventListener` → `removeEventListener`
- WebSocket / Subscription → close / unsubscribe
- Fetch s AbortController → `controller.abort()`

**Co cleanup NEpotřebuje:**
- `document.title = ...` (idempotentní přepsání, nic neběží v pozadí)
- `localStorage.setItem(...)` (jednou zapsal, hotovo)
- Synchronní výpočet

### Časté chyby s useEffect

**1. Infinite loop**
```tsx
const [count, setCount] = useState(0);
useEffect(() => {
    setCount(count + 1);   // setState v effect
});                         // bez deps → každý render → loop
// ⚠️ React: "Maximum update depth exceeded"
```

**Pravidlo:** pokud uvnitř useEffectu měníš state, **nedej ho do deps** (jinak loop).

**2. Stale closure**
```tsx
useEffect(() => {
    setUkoly([...ukoly, novy]);
}, []);   // ← `ukoly` chybí v deps → zachytí staré ukoly natrvalo
```

**Pravidlo:** **vše, co čteš z renderu a může se měnit, MUSÍ být v deps**.

**3. Race condition (fetch)**
```tsx
useEffect(() => {
    fetch("/api/users").then(r => r.json()).then(setUsers);
}, [userId]);
// ⚠️ Pokud user změní userId rychle, druhá response může přijít po první → pomotá data
```

**Oprava: AbortController + cleanup**
```tsx
useEffect(() => {
    const controller = new AbortController();
    fetch("/api/users", { signal: controller.signal })
        .then(r => r.json())
        .then(setUsers)
        .catch(e => { if (e.name !== "AbortError") throw e; });
    return () => controller.abort();
}, [userId]);
```

---

## `useRef` — reference + mutable bez re-render

Vrací **objekt se single property `.current`**. Objekt **přežívá rendery**, **změna `.current` NEVYVOLÁ re-render**.

### Use case 1: Reference na DOM element

```tsx
const inputRef = useRef<HTMLInputElement>(null);

useEffect(() => {
    inputRef.current?.focus();   // auto-focus při mount
}, []);

<input ref={inputRef} />
```

Optional chaining `?.` — pokud `current` není null, zavolej `.focus()`.

### Use case 2: Mutable hodnota bez re-render

```tsx
const timerId = useRef<number | null>(null);

const start = () => {
    timerId.current = setInterval(...);
};
const stop = () => {
    if (timerId.current) clearInterval(timerId.current);
};
```

Změna `timerId.current` neovlivní UI, ale **přežívá rendery**.

### useState vs useRef

| useState | useRef |
|---|---|
| Změna → re-render | Změna → **žádný re-render** |
| Pro UI data | Pro DOM ref / "hidden" mutable hodnoty |
| Synchronní | Synchronní |

---

## `useId` — unikátní stabilní ID

Vrací **unikátní string ID** napříč instance komponent + stabilní napříč rendery.

```tsx
const emailId = useId();
const heslaId = useId();

<label htmlFor={emailId}>Email</label>
<input id={emailId} />

<label htmlFor={heslaId}>Heslo</label>
<input id={heslaId} />
```

**Proč ne hardcode `id="email"`?**
- Použiješ komponentu 2× → duplicitní ID = HTML/a11y violation
- `Math.random()` se mění mezi rendery → label se odpojí

**Pravidlo:** **jedno `useId` per logické pole** (ne sdílet + appendovat suffix).

**Pozor:** v JSX **`htmlFor`** ne `for` (rezervované slovo JS).

---

## React Strict Mode

V dev (Vite default) **mountuje komponentu 2×** a **useEffecty spouští 2×** pro detekci side-effect bugů.

```
Mount → effect spustí (SETUP)
       → strict cleanup (CLEANUP)
       → effect spustí znova (SETUP)
```

**Detekuje:**
- Chybějící cleanup (timer pokračuje po unmount)
- Side effecty v render body (mimo useEffect)
- Resource leaky

**V produkci NEdělá** — dev-only feature.

**Pravidlo:** pokud tvůj kód rozbije dvojité spuštění, je tam bug. Strict mode tě nutí psát robustně.

---

## Časté chytáky komise

1. **Pravidla hooks 3** — top-level, jen z komponent, stejné pořadí
2. **Cleanup spouští se:** deps change NEBO unmount
3. **Infinite loop:** setState v useEffect bez správných deps
4. **Race condition:** fetch v useEffect potřebuje AbortController nebo flag
5. **useState vs useRef:** stav (re-render) vs mutable (no re-render)
6. **useId:** unikátní stabilní ID, **ne pro keys v map** (jen pro label-input páry)
7. **Strict Mode:** dev double-mount, prod ne

---

## Q&A pro komisi

**Q:** Co je hook?
**A:** Funkce začínající `use`, dává funkcionálním komponentám schopnosti tříd (stav, lifecycle, refs).

**Q:** Pravidla hooks?
**A:** Top-level only, jen z React komponent / custom hooks, stejné pořadí každý render.

**Q:** Rozdíl useState vs useRef?
**A:** useState — změna vyvolá re-render. useRef — drží **mutable hodnotu, která nezpůsobí re-render** + reference na DOM.

**Q:** Kdy se useEffect spustí?
**A:** Podle dep array. `[]` = po mount, `[x]` = po mount + změně x, bez deps = po každém renderu.

**Q:** Co je cleanup function?
**A:** Funkce vrácená z useEffectu. Spouští se **před dalším spuštěním efektu** (deps change) NEBO **při unmount**. Pro úklid timerů, listenerů, subscription.

**Q:** Co je useId a proč?
**A:** Unikátní stabilní ID per instance komponenty. Pro propojení `<label htmlFor>` a `<input id>` bez kolizí mezi instances.

**Q:** Co je React Strict Mode?
**A:** Dev-only feature, double-mountuje komponenty pro detekci side-effect bugů. V produkci se neaplikuje.
