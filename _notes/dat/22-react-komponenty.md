# DAT 22 — React komponenty a props

> **Cíl:** umět postavit multi-component React aplikaci s typed props a callback funkcemi.
> **Formát zkoušky:** 30 min praktika u PC + 15 min obhajoba. **Stack: React + TypeScript přes Vite.**

---

## Co je komponenta

**Komponenta = JS/TS funkce, která:**
1. Přijímá **`props`** jako argument (objekt)
2. Vrací **JSX** (popis UI)

Tagová syntaxe `<MyComponent prop={x} />` v JSX je **jen syntax cukr** pro volání `MyComponent({prop: x})`.

```tsx
function Pozdrav({ jmeno }: { jmeno: string }) {
    return <h1>Ahoj, {jmeno}!</h1>;
}

// Použití
<Pozdrav jmeno="Harry" />
```

**Pravidla pojmenování:**
- Komponenta **velkým písmenem** (`Pozdrav`), HTML element malým (`p`, `div`)
- React tak rozezná, co je co

---

## Setup nového projektu

```bash
npm create vite@latest nazev -- --template react-ts
cd nazev
npm install
npm run dev   # http://localhost:5173
```

Vygeneruje:
- `src/main.tsx` — entry point (`createRoot(...).render(<App />)`)
- `src/App.tsx` — root komponenta
- `index.html` — `<div id="root"></div>`
- `package.json` — dependencies + scripts (`dev`, `build`)

---

## Props (typované)

### Definice props typu

```tsx
type PozdravProps = {
    jmeno: string;
    vek: number;
    aktivni?: boolean;        // ? = volitelný
};
```

### Destructuring v argumentu

```tsx
export default function Pozdrav({ jmeno, vek, aktivni = false }: PozdravProps) {
    return <p>Ahoj, {jmeno}! Je ti {vek} let. {aktivni && "🟢"}</p>;
}
```

### Předání props v JSX

```tsx
<Pozdrav jmeno="Harry" vek={20} aktivni={true} />
//      string v ""    number v {}   boolean v {}
```

**Pravidlo:** **string v `""`**, ostatní (number, boolean, výrazy) **v `{}`**.

### Callback props (funkce dolů)

```tsx
type ButtonProps = {
    onKlik: () => void;            // funkce bez parametrů, nic nevrací
    onSmaz: (id: number) => void;  // funkce s parametrem id
};

<Button onKlik={() => console.log("klik")} />
```

---

## State (`useState`)

```tsx
const [pocet, setPocet] = useState<number>(0);
// Tuple: [aktuální hodnota, setter]

setPocet(pocet + 1);    // klasická hodnota
setPocet(p => p + 1);   // funkční update (bezpečnější při více kliknutí)
```

**Pravidlo:** **`useState` jen UVNITŘ komponenty**, ne mimo. Mimo by React nevěděl, ke které komponentě stav patří.

### Typed state s prázdným defaultem

```tsx
const [ukoly, setUkoly] = useState<Ukol[]>([]);   // explicit <Ukol[]>, jinak TS odvodí 'never[]'
const [user, setUser] = useState<User | null>(null);
```

---

## Lifting state up

Pokud **víc komponent potřebuje stejná data**, stav musí žít v **nejbližším společném předkovi**.

```
App (drží state ukoly + 3 callbacky)
 ├── TodoForm    ← dostává onPridej
 └── TodoList    ← dostává ukoly + onSmaz + onHotovo
     └── TodoItem ← dostává jeden ukol + callbacky
```

**Sourozenci spolu NEKOMUNIKUJÍ přímo.** Komunikují přes společného rodiče:
- **Data dolů** přes props
- **Funkce dolů** přes props (callbacky)
- **Děti VOLAJÍ callbacky** → efektivně posílají info nahoru

### One-way data flow (klasický chyták komise)

**Data v Reactu tečou SHORA DOLŮ** přes props. NE "od dětí k rodičům". Callbacky předané dolů + volané v dětech = informace nahoru, ale **technicky stále přes props shora**.

---

## Controlled inputs

```tsx
const [text, setText] = useState("");

<input
    value={text}                              // ← React řídí hodnotu
    onChange={e => setText(e.target.value)}   // ← React aktualizuje stav
/>
```

**Bez `onChange` → input "zamčený"** (React drží value, ale neaktualizuje stav, takže nelze psát).

### Form submit s preventDefault

```tsx
const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();   // zabrání page reloadu
    // ...
};

<form onSubmit={handleSubmit}>...</form>
```

Nebo se vyhneš form refresh přes `<button type="button">` (default je `type="submit"`).

---

## Renderování seznamu

```tsx
{ukoly.map(u => (
    <TodoItem key={u.id} ukol={u} onSmaz={onSmaz} />
))}
```

### Pravidla pro `key`

- **Stabilní** napříč rendery (ne `Math.random()`)
- **Unikátní** mezi sourozenci v daném `map`
- **Nejlepší:** unikátní ID z dat (`u.id`)
- **OK pro neměnné seznamy** (jen append): index `(u, i) => key={i}`
- **NIKDY** index pro seznamy s insertem/delete ze středu

`key` slouží React vnitřnímu **reconciliation algoritmu** — identifikace prvků mezi rendery. V reálném DOMu ho neuvidíš.

---

## Immutable update (KRITICKÉ)

**React porovnává reference, ne obsah.** Mutace = stejná reference = **no re-render**.

```tsx
// ❌ ZAKÁZÁNO
ukoly.push(novy);             // mutace, React nevidí změnu
setUkoly(ukoly);
ukoly[0].hotovo = true;       // mutace objektu
setUkoly(ukoly);

// ✓ SPRÁVNĚ — vždy NOVÉ pole/objekt
setUkoly([...ukoly, novy]);                                                    // add
setUkoly(ukoly.filter(u => u.id !== id));                                      // remove
setUkoly(ukoly.map(u => u.id === id ? {...u, hotovo: !u.hotovo} : u));         // update
```

**`{...obj, x: nové}`** = spread + override. Vznikne **nový objekt**, ne mutace.

---

## Event handlery — past s voláním

```tsx
// ❌ ZAVOLÁ HNED PŘI RENDERU
<button onClick={smazat(5)}>Smazat</button>

// ✓ ČEKÁ NA KLIK
<button onClick={() => smazat(5)}>Smazat</button>
```

`onClick` chce **funkci**, ne výsledek volání. Šipková funkce `() => ...` zabalí volání do funkce, která se zavolá AŽ při kliknutí.

---

## JSX pravidla

- **Jeden kořenový element** (nebo Fragment `<>...</>`)
- **Atributy camelCase** (`className`, `onClick`, `htmlFor`)
- **Místo `class`** je `className` (`class` je rezervované slovo JS)
- **Místo `for`** je `htmlFor` (`for` je for-cyklus)
- **JS výrazy v `{}`**, stringy v `""`
- **Samouzavírací tagy `<img />`** povinné (HTML by neuzavíral)
- **Komentáře `{/* ... */}`** (uvnitř JSX musí být v `{}`)

---

## Časté chytáky komise

1. **Komponenta = funkce + props + JSX.** Tag je jen syntax cukr.
2. **Data tečou SHORA DOLŮ** (props). Komunikace nahoru = callbacky volané v dětech.
3. **State patří do nejbližšího společného předka** (lifting state up).
4. **Imutabilita** — vždy spread / map / filter, nikdy `push`/`splice`/mutaci.
5. **`onClick={fn()}` vs `onClick={() => fn()}`** — první volá HNED, druhý čeká.
6. **`key` na elementech v `map`** — stabilní ID, ne index pro dynamické seznamy.
7. **Controlled input** = `value` + `onChange` páry. Bez `onChange` = "zamčený".

---

## Q&A pro komisi

**Q:** Co je komponenta v Reactu?
**A:** JS/TS funkce přijímající props a vracející JSX. Tag-like syntaxe je jen syntax cukr pro volání funkce.

**Q:** Rozdíl state vs props?
**A:** State = interní paměť komponenty, měnitelná setterem. Props = vstupní argumenty od rodiče, **immutable** z perspektivy dítěte.

**Q:** Co znamená "lifting state up"?
**A:** Stav patří do **nejbližšího společného předka** komponent, které ho potřebují číst nebo měnit. Předává se dolů přes props.

**Q:** Jakým směrem tečou data v Reactu?
**A:** **Shora dolů** (unidirectional). Rodič → dítě přes props. Komunikace zpět přes callbacky předané z rodiče dolů, volané v dítěti.

**Q:** Proč immutable update?
**A:** React porovnává **reference**, ne obsah. Mutace = stejná reference = React si myslí, že nic nezměnilo, nepřekreslí. Vždy nové pole/objekt.

**Q:** K čemu je `key` v `.map`?
**A:** Identita prvku napříč rendery. React přes ni v reconciliation pozná, který prvek se změnil/přidal/smazal/přesunul.
