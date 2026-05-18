---
title: SWI 24 — Programovací jazyky
description: Definice, úrovně, kompilace/interpret/hybrid, JIT/AOT, paradigmata, procedurální/deklarativní, syntaxe, architektura .NET
tags: [maturita, swi, jazyky, kompilace, dotnet, paradigmata]
---

# Q: Co je programovací jazyk?
A: **Formální jazyk pro zápis algoritmů** — prostředek pro dávání instrukcí počítači. Má **syntaxi** (pravidla psaní), **sémantiku** (význam) a **klíčová slova** (`if`, `while`, `class`).

# Q: Úrovně programovacích jazyků?
A: **Nízkoúrovňové** = blízko HW (Assembler, strojový kód). **Vysokoúrovňové** = srozumitelné pro člověka (C#, Python, Java). **Esoterické** = pro zábavu (Brainfuck).

# Q: 3 způsoby zpracování programovacích jazyků?
A: **Kompilované** (C, C++, Pascal) — celý kód → strojový kód naráz. **Interpretované** (Python, PHP) — řádek po řádku za běhu. **Hybridní** (C#, Java) — zdroják → mezikód → VM s JIT.

# MCQ: Která jazyk je hybridní?
- Python
- C
- !C#
- Bash
> C# se kompiluje do IL (mezikód), CLR ho přes JIT převede na native code. Python je interpret (s JIT optimalizacemi v moderních verzích).

# Q: Výhody a nevýhody kompilovaných jazyků?
A: **+** Rychlost (běží přímo na HW), optimalizace, chyby při kompilaci. **−** Rekompilace pro každý OS, pomalejší vývoj.

# Q: Výhody a nevýhody interpretovaných jazyků?
A: **+** Přenositelnost (stačí interpret), flexibilita, rychlý vývoj bez kompilace. **−** Pomalejší běh, chyby až za běhu.

# Q: Jak fungují hybridní (manažované) jazyky?
A: 1) Zdroják se **kompiluje do mezikódu** (IL/Bytecode). 2) Mezikód spustí **VM** (CLR pro .NET, JVM pro Javu). 3) VM přes **JIT** převede na native code za běhu. Výhody: multiplatformnost + rychlost.

# CLOZE: .NET mezikód = {{IL (Intermediate Language)}}. Java mezikód = {{Bytecode}}.

# Q: Co je CLR?
A: **Common Language Runtime** — virtuální stroj .NET. Spravuje běh kódu, **garbage collector**, **JIT compiler** (IL → native). Ekvivalent JVM v Javě.

# Q: Co je JIT compilation?
A: **Just-In-Time** — překlad mezikódu na nativní strojový kód **za běhu**, právě v okamžiku, kdy je metoda volaná poprvé. Optimalizuje pro aktuální CPU.

# Q: Co je AOT compilation a kdy se hodí?
A: **Ahead-Of-Time** — překlad mezikódu na native **před spuštěním** (kompilace už dělá native binary). Rychlejší startup, menší velikost. .NET 7+ podporuje pro production.

# CLOZE: JIT překládá {{za běhu}}, AOT {{před spuštěním}}.

# Q: 4 hlavní paradigmata?
A: **Imperativní** (krok za krokem, C/BASIC), **Objektově orientované** (objekty s atributy a metodami, C#/Java/Python), **Funkcionální** (matematický výpočet, F#/Haskell), **Logické** (pravidla a fakta, Prolog).

# Q: Co je multi-paradigmatický jazyk?
A: Jazyk, který **podporuje víc paradigmat současně**. C# umí imperativní, OOP i funkcionální. Většina moderních jazyků je multi-paradigmatických.

# Q: Co je procedurální vs deklarativní programování?
A: **Procedurální** = píšeš **KROK ZA KROKEM, CO se má stát** (C, OOP). **Deklarativní** = píšeš **CO chceš získat, neřešíš JAK** (SQL, Haskell, Excel).

# MCQ: SQL je jakého typu jazyka?
- Procedurální
- !Deklarativní
- Objektově orientovaný
- Imperativní
> `SELECT * FROM users` říká CO chceš, ne JAK to DB má udělat. DB query planner vybere algoritmus.

# Q: 3 hlavní styly syntaxe?
A: **C-derived** (závorky `{}`, středníky `;`) — C/C++/C#/Java/JS/PHP. **Odsazovací** — Python (logika přes odsazení). **Keyword-based** — Pascal (`begin ... end`), Lua.

# Q: Co je multiplatformnost a jak ji dosáhnout?
A: Schopnost jazyka/aplikace **běžet na různých OS**. Způsoby: 1) **Kompilace pro každý OS** (C, Rust). 2) **Mezikód + VM** (.NET, Java). 3) **Interpret** (Python, JS). 4) **WebAssembly** (Rust/C++ → WASM v prohlížeči).

# Q: 3 hlavní verze .NET?
A: **.NET Framework** (legacy, Windows-only). **.NET (Core)** moderní multiplatformní (Windows/Linux/Mac). **Mono** open-source implementace (umožnila C# na mobilech).

# Q: 4 klíčové komponenty .NET architektury?
A: **CLR** (Common Language Runtime, VM), **IL** (Intermediate Language, mezikód), **JIT compiler** (IL → native), **BCL** (Base Class Library, standardní knihovna).

# CODE: Flow kompilace C# aplikace
```
C# zdroják (.cs)
    ↓ Roslyn kompilátor
IL mezikód (.dll / .exe)
    ↓ CLR načte, JIT překládá
Strojový kód (CPU instructions)
    ↓ vykoná procesor
```

# Q: 4 hlavní využití .NET?
A: **ASP.NET** (web aplikace), **MAUI/Xamarin** (mobilní iOS/Android), **Unity** (herní engine), **WPF/WinForms** (desktop).

# MCQ: Pokud chceš napsat 1 kód, který běží na Windows i Linuxu, použiješ...
- C s gcc pro každý OS
- !.NET / Java (mezikód + VM)
- Pouze JavaScript v prohlížeči
- Assembler
> Mezikód běží všude, kde je VM. .NET (Core) má CLR pro Win/Linux/Mac.

# Q: Co je garbage collector?
A: **Automatický správce paměti**. Sleduje, které objekty se už nepoužívají (žádné reference), a uvolňuje paměť. Programátor se nemusí starat o `delete`/`free`. V .NET, Javě, Pythonu, JS. NE v C/C++/Rust (ruční nebo ownership system).

# Q: C# klíčová slova — vyjmenuj 5 příkladů?
A: `if`, `while`, `for`, `class`, `interface`, `public`, `private`, `static`, `void`, `return`, `using`, `namespace`, `var`. Rezervovaná slova, nelze je použít jako identifikátor.

# FREE: Popis cestu C# kódu od zdrojáku k procesoru.
> 1) Programátor napíše C# zdroják (`.cs` soubor). 2) **Roslyn kompilátor** (součást .NET SDK) přečte zdroják a zkontroluje **syntaxi** (závorky, středníky, klíčová slova) + **typy** (statické typování). 3) Pokud OK, vygeneruje **IL (Intermediate Language)** = mezikód. Výsledek je `.dll` nebo `.exe` soubor. 4) Při spuštění **CLR (Common Language Runtime)** načte soubor. 5) **JIT compiler** převede IL na **nativní strojový kód** pro aktuální CPU (x86, ARM) — dělá to **per metodu**, právě když se má volat (lazy). 6) Procesor vykoná instrukce. **Garbage collector** běží na pozadí a uklízí nepoužívanou paměť. Důsledek: stejný `.dll` běží na Windows i Linuxu (CLR existuje pro oba).

# FREE: Vysvětli rozdíl mezi procedurálním a deklarativním přístupem s příkladem.
> **Procedurální** říká **JAK** udělat věc — instrukce krok za krokem. Příklad (C#): "vytvoř proměnnou součet=0, iteruj všechna čísla v poli, ke každému přidej součet, na konci vrať součet". Programátor řídí algoritmus. **Deklarativní** říká **CO** chceš — bez detailu implementace. Příklad (LINQ/SQL): `numbers.Sum()` nebo `SELECT SUM(value) FROM numbers`. Programátor specifikuje výsledek, runtime/DB vybere algoritmus. Deklarativní bývá kratší a čitelnější, ale méně kontroly nad výkonem.
