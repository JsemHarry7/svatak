---
title: SWI 1 — Diagramy UML
description: UML 14 typů (7 strukturálních + 7 behaviorálních), class diagram, vztahy, multiplicita, use case, activity, state machine, sequence
tags: [maturita, swi, uml, design, diagramy]
---

# Q: Co je UML?
A: **Unified Modeling Language** — standardizovaný grafický jazyk pro vizualizaci, návrh a dokumentaci softwarových systémů. Spravuje ho **OMG** (Object Management Group).

# Q: Kolik typů diagramů UML definuje a v jakých kategoriích?
A: **14 typů** ve dvou kategoriích: **7 strukturálních** (z čeho je systém složený) + **7 behaviorálních** (jak systém funguje).

# CLOZE: Strukturální diagramy odpovídají na otázku {{z čeho je systém složený}}. Behaviorální na {{jak systém funguje}}.

# Q: 5 nejpoužívanějších UML diagramů?
A: **Class** (třídy + atributy + metody + vztahy), **Use Case** (co může uživatel se systémem dělat), **Activity** (tok aktivit), **State Machine** (stavy objektu), **Sequence** (časová posloupnost zpráv).

# MCQ: Class diagram patří do kategorie...
- !Strukturální
- Behaviorální
- Komunikační
- Hybridní
> Popisuje statickou strukturu (z čeho je systém složený) → strukturální.

# MCQ: Use Case diagram patří do kategorie...
- Strukturální
- !Behaviorální
- Komunikační
- Hybridní
> Popisuje, co může uživatel se systémem dělat (chování) → behaviorální.

# Q: 4 visibility modifiers v UML class diagramu?
A: **`+`** public, **`-`** private, **`#`** protected, **`~`** package (internal v C#).

# CLOZE: V UML class diagramu: `+` = {{public}}, `-` = {{private}}, `#` = {{protected}}, `~` = {{package/internal}}.

# Q: Jak se v class diagramu značí abstraktní třída?
A: Název **kurzívou** nebo stereotyp **`<<abstract>>`** nad názvem. Abstraktní metoda taky kurzívou.

# Q: Jak se značí statický člen v UML?
A: **Podtržením** (text je underlined).

# Q: Jak se značí rozhraní (interface)?
A: Stereotypem **`<<interface>>`** nad názvem třídy. Implementace přerušovanou čárou se šipkou (realizace).

# Q: 6 typů vztahů v UML class diagramu?
A: **Asociace** (──), **agregace** (──◇), **kompozice** (──◆), **generalizace** (──▷, dědičnost), **realizace** (- - ▷, implementace rozhraní), **závislost** (- - ▶, A používá B).

# Q: Rozdíl mezi agregací a kompozicí?
A: Obě jsou vztah celek-část. **Agregace** (prázdný `◇`) = část **přežije** celek (Tým ◇ Hráč). **Kompozice** (plný `◆`) = část **umírá s celkem** (Dům ◆ Pokoj).

# CLOZE: Agregace `◇` = část {{přežije}} celek. Kompozice `◆` = část {{umírá s}} celkem.

# MCQ: Která UML čára značí dědičnost?
- Plná, prázdný kosočtverec
- Plná, plný kosočtverec
- !Plná, prázdná trojúhelníková šipka (▷)
- Přerušovaná, prázdná šipka
> Plná čára + prázdný trojúhelník (▷) = generalizace = dědičnost (extends). Šipka míří na rodiče.

# Q: Jak se značí implementace rozhraní v UML?
A: **Přerušovaná čára** + **prázdná trojúhelníková šipka** (- - ▷). Říká se tomu **realizace**. Třída realizuje rozhraní.

# Q: Co je multiplicita a jak se zapisuje?
A: **Kardinalita vztahu** — kolik objektů jednoho typu se váže k objektu druhého typu. Zápisy: **`1`** (právě jeden), **`0..1`** (nepovinný), **`*`** nebo **`0..*`** (libovolný počet), **`1..*`** (alespoň jeden), **`2..5`** (rozsah).

# CODE: Multiplicita příklad
```
[Student] 1 ──── 1..* [Kurz]     // student v 1+ kurzech, kurz má 1+ studentů
[Auto]    1 ──── 4    [Kolo]     // auto má přesně 4 kola
[Osoba]   0..1 ── 0..* [Telefon] // telefon má 0-1 majitele, osoba libovolně telefonů
```

# Q: Co je Use Case diagram a co ukazuje?
A: Vizualizuje **interakce mezi aktérem (uživatelem) a systémem**. Odpovídá: *"Co může uživatel se systémem dělat?"*. Aktér = postavička, use case = elipsa, system boundary = obdélník.

# Q: Rozdíl mezi `<<include>>` a `<<extend>>`?
A: **`<<include>>`** = use case A **VŽDY** zahrnuje B (Objednat vždy zahrnuje Přihlášení). **`<<extend>>`** = use case B **VOLITELNĚ** rozšiřuje A (Přihlášení může být rozšířeno o Zapomenuté heslo).

# CLOZE: `<<include>>` = use case A {{vždy}} zahrnuje B. `<<extend>>` = use case B {{volitelně}} rozšiřuje A.

# Q: Co je Activity diagram?
A: **Moderní verze vývojového diagramu** — popisuje **tok aktivit**. Vhodný pro business procesy a algoritmy. Klíčové: ● start, ⬤ konec, zaoblený obdélník = aktivita, ◇ kosočtverec = větvení (s `[guard]`), ▬ tlustá čára = paralelní rozdělení/spojení.

# Q: Co jsou swimlanes v Activity diagramu?
A: **Sloupce** rozdělující diagram podle **toho, kdo aktivitu provádí** (oddělení, role). Např. sloupce: Zákazník | Prodejce | Sklad.

# Q: Co je State Machine diagram?
A: Popisuje **životní cyklus objektu** — stavy, ve kterých může být, a přechody mezi nimi. Přechod značen `událost [guard] / akce`.

# Q: Co je Sequence diagram?
A: **Časová posloupnost zpráv mezi objekty**. Kdo komu volá a v jakém pořadí. **Čas plyne shora dolů.**

# Q: Co je lifeline v Sequence diagramu?
A: **Svislá přerušovaná čára** představující **časovou osu jednoho objektu**. Activation bar = úzký obdélník na lifeline = doba, kdy objekt něco vykonává.

# Q: Rozdíl mezi synchronní a asynchronní zprávou v Sequence diagramu?
A: **Synchronní** (plná šipka) — volání **čeká** na návrat. **Asynchronní** (otevřená šipka) — volání **nečeká**, pokračuje dál.

# Q: 4 nejpoužívanější combined fragments v Sequence diagramu?
A: **`alt`** (alternativa = if-else), **`opt`** (volitelná akce = if), **`loop`** (cyklus), **`par`** (paralelní vykonání).

# Q: Je ER diagram součástí UML?
A: **Ne.** ER diagram (Entity-Relationship) je **samostatná notace** (Chen 1976), dnes nejčastěji v **Crow's Foot** notaci. UML je pro OOP design, ER pro databáze.

# Q: 3 typy kardinality v ER diagramu?
A: **1:1** (jeden ku jednomu — student má 1 index), **1:N** (jeden ku víc — učitel má víc kurzů), **N:M** (víc ku víc — studenti zapisují víc kurzů).

# Q: Jak se v relační DB modeluje vztah N:M?
A: **Vazební tabulka** (junction table) s **dvojicí cizích klíčů** na obě entity. Příklad: `STUDENT`, `KURZ`, `ZAPIS(student_id, kurz_id, datum)`.

# MCQ: Komise: "Vidíš na diagramu šipku s plným kosočtvercem mezi A a B. Co to znamená?"
- A je závislé na B
- A implementuje B
- !A obsahuje B kompozicí (B umírá s A)
- A dědí z B
> Plný kosočtverec ◆ = kompozice. Část (B) má životní cyklus svázaný s celkem (A).

# MCQ: Pokud máš diagram s ● → [Krok 1] → ◇ → ⬤, je to...
- Class diagram
- Sequence diagram
- !Activity diagram
- Component diagram
> Začátek (●), aktivita, větvení (◇), konec (⬤) = Activity diagram (tok kroků).

# MCQ: Diagram, který ukazuje čas plynoucí shora dolů a zprávy mezi objekty?
- Activity
- Class
- !Sequence
- Use Case
> Sequence diagram = časová osa + zprávy. Čas shora dolů, lifeline = svislá čára objektu.

# FREE: Vyjmenuj 5 nejpoužívanějších UML diagramů a vysvětli, co každý ukazuje.
> 1) **Class** — třídy, atributy, metody, vztahy. Statická struktura systému. Nejdůležitější. 2) **Use Case** — interakce aktér ↔ systém, "co může uživatel dělat". Aktér + use case + system boundary. 3) **Activity** — tok aktivit, moderní vývojový diagram. Pro business procesy a algoritmy. Větvení přes ◇. 4) **State Machine** — životní cyklus objektu, stavy + přechody. 5) **Sequence** — časová posloupnost zpráv mezi objekty, čas shora dolů.

# FREE: Co by měl obsahovat dobrý class diagram pro programátora?
> Pro každou třídu obdélník se 3 sekcemi: **název** (případně stereotyp), **atributy** (s visibility +/-/# a typem), **metody** (s parametry, návratovým typem, visibility). Plus **vztahy mezi třídami** se značkami (◇ agregace, ◆ kompozice, ▷ generalizace, - - ▷ realizace), **multiplicita** na koncích (1, *, 0..1, 1..*), **role** ve vztahu pokud je relevantní. Speciální značení: kurzíva pro abstraktní, podtržení pro statické, `<<interface>>` stereotyp.
