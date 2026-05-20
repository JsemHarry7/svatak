---
title: SWI 18 — React teorie
description: Knihovna × framework, DOM/Virtual DOM/Shadow DOM, JSX, transpilace, toolchain, funkcionální komponenty, hooks, React 19
tags: [maturita, swi, react, javascript, frontend]
---

# Q: Co je React?
A: **JavaScriptová knihovna pro tvorbu uživatelských rozhraní.** Vyvinula firma **Meta** (Facebook), poprvé 2013. Aktuální verze **React 19** (prosinec 2024).

# Q: 4 klíčové vlastnosti Reactu?
A: **Komponentový přístup** (znovupoužitelné bloky UI), **deklarativní** (popisuješ stav, ne změny), **Virtual DOM** (optimalizace překreslování), **unidirectional data flow** (data shora dolů přes props).

# Q: Rozdíl knihovna × framework?
A: **Knihovna** = sada funkcí, **TY voláš JI** (React, jQuery). **Framework** = strukturovaná aplikace, **ONA volá TVŮJ kód** (Next.js, Angular). Dnes hranice rozmazaná.

# CLOZE: React je {{knihovna}} (jen UI), Next.js je {{framework}} (routing, SSR, RSC).

# Q: Co je DOM?
A: **Document Object Model** — stromová reprezentace HTML dokumentu v paměti prohlížeče. Každý element = uzel (objekt), JS s ním manipuluje.

# Q: Co je Virtual DOM?
A: **Odlehčená kopie DOMu v JS paměti.** React s ní pracuje místo přímé manipulace s reálným DOMem. **Optimalizace překreslování** přes diff a apply.

# Q: Princip Virtual DOM v 3 krocích?
A: 1) Změna stavu → React přepočítá Virtual DOM. 2) Porovná s předchozím stavem (**diff algoritmus**). 3) Aplikuje **jen rozdíly** na reálný DOM.

# Q: Co je Shadow DOM?
A: **Nativní webová technologie** pro **izolaci stylů a struktury uvnitř komponenty**. Vytvoří oddělený DOM strom uvnitř prvku, skrytý před zbytkem stránky. Použití: **Web Components**, `<video>`, `<input type="range">`.

# MCQ: Klasický maturitní chyták — React používá:
- !Virtual DOM
- Shadow DOM
- Oba
- Žádný z nich
> React má **JEN Virtual DOM** pro optimalizaci. Shadow DOM je pro **izolaci stylů** (Web Components), React místo něj má CSS Modules / Tailwind / styled-components. Záměnka je klasický chyták komise.

# Q: Rozdíl Virtual DOM × Shadow DOM?
A: **Virtual DOM** = **výkon** (kopie v JS paměti pro optimalizaci překreslování). **Shadow DOM** = **izolace** (stylů a struktury uvnitř komponenty). **Úplně jiné věci**, jen podobné názvy.

# Q: Co je JSX?
A: **Rozšíření JavaScript syntaxe** pro psaní HTML-like kódu v JS souboru. **NENÍ HTML, NENÍ čistý JS.** **Babel/SWC** ho transpiluje na volání `React.createElement()`.

# CODE: JSX vs to, co z něj vznikne
```jsx
// Co píšeš
const element = <h1 className="nadpis">Ahoj, {jmeno}!</h1>;

// Co vznikne po transpilaci
const element = React.createElement(
  "h1",
  { className: "nadpis" },
  "Ahoj, ", jmeno, "!"
);
```

# Q: 7 pravidel JSX?
A: 1) Jeden kořenový element (nebo Fragment `<>`). 2) Atributy camelCase (`className`, `onClick`). 3) Vyhrazená slova přejmenovaná (`htmlFor` ne `for`). 4) JS výrazy v `{}`. 5) Samouzavírací tagy `<img />`. 6) Inline styles jako objekty. 7) Komentáře `{/* */}`.

# CLOZE: V JSX místo HTML `class` píšeš {{className}}, místo `for` píšeš {{htmlFor}}.

# Q: Proč v JSX `className` a ne `class`?
A: **`class` je rezervované slovo v JavaScriptu** (pro definici tříd). JSX se transpiluje na JS, kde by `class="..."` bylo chybou. Stejně `for` (`htmlFor`) je rezervované pro for cyklus.

# Q: Proč potřebuje `key` v JSX listech?
A: React ho používá k **identifikaci, který prvek se změnil/přidal/smazal** mezi rendery. Bez něj může React **překreslit zbytečně moc** nebo se zmást při změně pořadí.

# MCQ: Nejlepší volba pro `key` v listu?
- Index pole `(item, i) => key={i}`
- `Math.random()`
- !Unikátní ID z dat `item.id`
- Pořadí v abecedě
> Unikátní ID je stabilní napříč rendery. Index funguje jen pro neměnné seznamy. Math.random() generuje nový key každý render = peklo pro React.

# Q: Co je transpilace?
A: **Source-to-source překlad** zdrojového kódu z jednoho jazyka do jiného (stále čitelného). Příklady: JSX → JS, TypeScript → JS, moderní ES2024 → ES2015.

# Q: Rozdíl transpilace × kompilace?
A: **Kompilace** = překlad na **strojový kód** (binární, pro CPU). C → exe. **Transpilace** = překlad na **jiný zdrojový kód** (čitelný). JSX → JS.

# CLOZE: Babel → klasický transpiler. {{SWC}} → Rust, 10-20× rychlejší, default ve Vite/Next.js. {{esbuild}} → Go, ještě rychlejší.

# Q: 4 hlavní transpilery v React ekosystému?
A: **Babel** (klasika), **SWC** (Rust, 10-20× rychlejší), **esbuild** (Go), **tsc** (TypeScript compiler).

# Q: 3 JS engines v prohlížečích?
A: **V8** (Chrome, Edge, Opera — Google). **SpiderMonkey** (Firefox — Mozilla). **JavaScriptCore / Nitro** (Safari — Apple).

# Q: Co je event loop?
A: JS je **single-threaded**, ale **asynchronní díky event loopu**. Dlouhé operace (HTTP, timery) se vykonávají mimo hlavní vlákno, callback se zařadí do **fronty** (queue), až je hotový.

# Q: Microtasks × macrotasks?
A: **Microtasks** (Promise.then) mají **vyšší prioritu** než **macrotasks** (setTimeout). V cyklu event loopu: vše synchronní → všechny microtasks → 1 macrotask → render → opakuj.

# CODE: Event loop priority
```javascript
console.log('1');
setTimeout(() => console.log('2'), 0);
Promise.resolve().then(() => console.log('3'));
console.log('4');
// Output: 1, 4, 3, 2
```

# Q: Co je toolchain v Reactu?
A: **Sada vývojářských nástrojů** spolupracujících při vývoji. **Node.js** (runtime), **npm** (package manager), **Vite/Webpack** (bundler), **Babel/SWC** (transpiler), **ESLint** (linter), **Prettier** (formátování), **TypeScript** (typy).

# Q: Jak vytvořit React projekt v 2026?
A: **Vite** (pure React SPA): `npm create vite@latest moje-aplikace -- --template react-ts`. **Next.js** (web aplikace): `npx create-next-app@latest moje-aplikace`. **`create-react-app` je DEPRECATED od 2023.**

# Q: Co je `package.json`?
A: **Manifest projektu.** Obsahuje jméno, verzi, scripts (`dev`, `build`), `dependencies` (runtime), `devDependencies` (jen pro build).

# CLOZE: `dependencies` = běží v {{produkci/runtime}}. `devDependencies` = jen při {{buildu/vývoji}}.

# Q: Co je funkcionální komponenta?
A: **JS funkce, která přijímá `props` a vrací JSX.** Příklad: `const Pozdrav = ({ jmeno }) => <h1>Ahoj, {jmeno}!</h1>;`. Standard od React 16.8 (2019, hooks).

# CODE: Funkcionální komponenta s destructuringem
```jsx
const Pozdrav = ({ jmeno, vek }) => {
  return (
    <div>
      <h1>Ahoj, {jmeno}!</h1>
      <p>Je ti {vek} let.</p>
    </div>
  );
};

// Použití
<Pozdrav jmeno="Jan" vek={20} />
```

# Q: Co jsou props?
A: **Vstupní data komponenty** od rodiče. **Immutable** (komponenta je NESMÍ modifikovat). Předávají se jako atributy: `<Komponenta prop1="hodnota" prop2={42} />`.

# Q: Co je `children` prop?
A: **Speciální prop**, který obsahuje **obsah mezi otevíracím a uzavíracím tagem** komponenty. `<Karta>obsah</Karta>` → `Karta` dostane `children: 'obsah'`.

# Q: Rozdíl třídní × funkcionální komponenta?
A: **Třídní (legacy):** `class X extends Component`, `this.state`, `this.setState`, lifecycle methods. **Funkcionální (moderní):** funkce, `useState()`, `useEffect()`, méně boilerplate. **Standard 2026 = funkcionální.**

# Q: 4 hlavní hooks?
A: **`useState`** (lokální stav), **`useEffect`** (vedlejší efekty), **`useContext`** (globální stav bez prop drilling), **`useRef`** (reference na DOM nebo mutable hodnota bez re-renderu).

# Q: 2 pravidla hooks?
A: 1) Volat **jen na nejvyšší úrovni** komponenty (NE v cyklech, podmínkách, nested funkcích). 2) Volat **jen z React komponent nebo custom hooks**, ne z obyčejných funkcí.

# Q: Co jsou React Server Components (RSC)?
A: **Komponenty renderované na serveru, ne v prohlížeči.** Klient dostává hotové HTML, ne JS. **Defaultní v Next.js App Router** (React 19). Můžou volat DB/API přes `async/await`. **Nemůžou používat hooky** (useState, useEffect).

# Q: Rozdíl Server Component × Client Component?
A: **Server:** renderuje na serveru, **bez hooks**, může `async/await` přímo, menší JS bundle, defaultní v Next.js App Router. **Client:** renderuje v prohlížeči, **má hooks**, vyžaduje `"use client"` direktivu, větší bundle.

# Q: Co je React Compiler?
A: **Nový kompilátor v React 19** (dřív "React Forget"). **Automaticky memoizuje** komponenty a hodnoty. **Konec ručního `useMemo`/`useCallback`/`React.memo`** — píšeš obyčejný JS, kompilátor optimalizuje.

# FREE: Popis celý flow renderování React komponenty s prop změnou.
> 1) Rodič renderuje `<Komponenta prop="A" />`. 2) React zavolá funkci `Komponenta({prop: "A"})`, ta vrátí JSX `<div>A</div>`. 3) React převede JSX na Virtual DOM strukturu. 4) Diff s předchozím Virtual DOM stavem — pokud prázdný (první render), aplikuje vše na reálný DOM. 5) Browser vykreslí `<div>A</div>` uživateli. 6) Rodič změní `prop="B"`. 7) React zavolá `Komponenta({prop: "B"})` znovu, vrátí JSX `<div>B</div>`. 8) Diff: změnil se text. 9) React aktualizuje JEN ten text v reálném DOMu (ne celý strom). 10) Browser repaint jednoho elementu, ne celé stránky.

# FREE: Vysvětli rozdíl mezi Virtual DOM a Shadow DOM s konkrétním use case.
> **Virtual DOM** je **technika optimalizace** používaná Reactem (a Vue do v3). Účel: minimalizovat drahé manipulace s reálným DOMem. **Mechanismus:** React drží v paměti odlehčenou JS kopii DOMu. Při změně stavu přepočítá Virtual DOM, diffem zjistí rozdíly, na reálný DOM aplikuje jen ty. **Use case:** dashboardy s často měnícími se daty, formuláře, SPA. **Shadow DOM** je **nativní web API** pro **izolaci**. Účel: kompletní zapouzdření stylů a struktury uvnitř komponenty. **Mechanismus:** prvek má `attachShadow()`, vytvoří se oddělený DOM strom, který je zvenku neviditelný. **Use case:** **Web Components** (vlastní HTML tagy s vlastními styly), `<video>` (controls jsou v Shadow DOM, takže CSS stránky je neovlivní). **React NEPOUŽÍVÁ Shadow DOM**, má jiné izolační techniky (CSS Modules, styled-components, Tailwind atomic classes).
