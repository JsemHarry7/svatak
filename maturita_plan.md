# Maturitní plán — P4A — 7.5. → 25.5.2026

**Filozofie:** Completion-based, ne time-based. Pesimismus = každé téma začínám od nuly, otestuju se, pokud sedí → další. Pokud ne → zůstávám.

**Tři tracky paralelně:**
1. SWI (25 témat) + DAT (25 témat) — sloučené do 8 bloků kvůli překryvu
2. ČJL (20 knih) — večerní / odpočinkový režim, audio/film jako vstup
3. DAT praktické drilování — vlastní samostatný track posledních ~5 dní

---

## Bloky (8 + ČJL)

Pořadí je voleno tak, aby každý blok stavěl na předchozím. Foundations → web → backend → frameworky → specializace.

### Blok A — Programovací základy (8 témat)
Foundation pro úplně všechno ostatní.

- SWI 2 — Algoritmus, složitost
- SWI 3 — Reprezentace dat (číselné soustavy, znaky, typy)
- SWI 4 — Datové typy, proměnné ↔ DAT 8 — Datové typy a pole *(spárovat)*
- DAT 10 — Podprogramy a lambda
- DAT 11 — Kolekce (stack, queue, dictionary)
- SWI 9 — OOP (dědičnost, polymorfismus, generika)
- DAT 9 — Spojové struktury, stromy

### Blok B — Web frontend (12 témat — největší blok)
Velký překryv SWI/DAT. SWI dává teorii, DAT prakticky.

- SWI 13 — Internet (URL, MIME, DNS)
- SWI 15 — Webová stránka (HTML tagy) ↔ DAT 1 — HTML5 a sémantika *(spárovat)*
- SWI 14 — Návrh obsahového webu (UX, SEO, responzivita)
- SWI 16 — CSS kaskáda, specificita
- SWI 17 — Vlastnosti CSS (jednotky, barvy, písma)
- DAT 2 — Bootstrap a grid systém
- DAT 3 — Flexbox
- DAT 4 — CSS Grid
- DAT 5 — Pozicování a z-index
- DAT 6 — CSS animace a transformace
- DAT 7 — Tabulky v HTML

### Blok C — Databáze (5 témat)
Čistý, soudržný blok.

- SWI 10 — Databáze (SQL/noSQL, klíče, integrita)
- SWI 11 — Normalizace (1NF, 2NF, 3NF, BCNF)
- SWI 12 — Jazyk SQL (DDL, DML, DCL, TCL)
- DAT 15 — ER model a návrh
- DAT 16 — SQL výběr a filtrování *(praktické)*

### Blok D — HTTP, REST, Auth (3 témata)
Bridge mezi frontendem a backendem.

- SWI 19 — Webové aplikace (SPA/MPA, HTTP, návratové kódy)
- SWI 21 — RESTful (AJAX, JSON, Web API)
- SWI 20 — Ověřování identity (JWT, OAuth2, OpenID)

### Blok E — ASP.NET (5 témat)
Velký praktický blok. Tady jsou 4 z 5 DAT témat hands-on.

- SWI 22 — ASP.NET teorie (Razor Pages, syntaxe, služby)
- DAT 18 — Razor Pages, GET/POST, binding *(praktické)*
- DAT 19 — Tag Helpers a formuláře *(praktické)*
- DAT 17 — REST API v ASP.NET *(praktické)*
- DAT 21 — ORM, migrace *(praktické)*

### Blok F — React + Next.js (6 témat)
Většina DAT témat zde je praktická.

- SWI 18 — React teorie (DOM, JSX, transpilace, toolchain)
- DAT 22 — Komponenty a props *(praktické)*
- DAT 23 — Hooks (useState, useEffect, useRef) *(praktické)*
- DAT 24 — React Router *(praktické)*
- DAT 25 — Správa stavu (useContext, useReducer) *(praktické)*
- DAT 20 — Next.js, SSR vs CSR

### Blok G — Diagramy, vzory, kvalita (4 témata)
Lehčí kognitivní zátěž — dobré jako "přepínač" mezi těžkými bloky.

- SWI 1 — UML diagramy
- SWI 5 — Návrhové vzory (Singleton, Factory, Observer, Command)
- SWI 6 — Chyby, testování, ladění
- SWI 23 — Událostmi řízené programování (event/subscriber/publisher)

### Blok H — Specializace (7 témat)
Standalone témata, dělá se na konci.

- SWI 7 — Šifrování a kódování
- SWI 8 — Kryptosystémy (TLS/SSL, podpis, otisk)
- DAT 12 — Souborový systém a streamy
- DAT 13 — Paralelní a asynchronní programování
- DAT 14 — Git a GitHub
- SWI 24 — Programovací jazyky (překlad, .NET, mezikód)
- SWI 25 — Architektura Android aplikací

### Track ČJL (20 knih)
Spárováno s audio/video pro pasivní příjem. Viz sekce ČJL níže.

---

## Pořadí bloků (timeline)

Days 1–17 = 8.5. → 24.5. Den 18 (25.5.) = ráno light review, odpoledne zkouška.

| Dny | Blok | Témat | Tempo |
|-----|------|-------|-------|
| Dnes (7.5.) | **Setup** | — | Audit + file systém + toolchain |
| 1–3 (8–10.5.) | A — Foundations | 8 | ~3/den |
| 4–7 (11–14.5.) | B — Web frontend | 12 | 3/den |
| 8–9 (15–16.5.) | C — Databáze | 5 | ~2,5/den |
| 10 (17.5.) | D — HTTP/REST/Auth | 3 | 3 |
| **11 (18.5.)** | **OBHAJOBA MP dopoledne** + SWI 22 ASP.NET teorie odpoledne | 1 | Light load. ČJL Steinbeck večer (dekompresní) |
| 12–13 (19–20.5.) | E — ASP.NET praxe (DAT 17, 18, 19, 21) | 4 | 2/den hands-on |
| 14–15 (21–22.5.) | F — React/Next.js | 6 | 3/den + reálný kód |
| 16 (23.5.) | G — Diagramy/vzory + H část 1 (šifrování, Git) | 7 | ⚠️ hutný den |
| 17 (24.5.) | H část 2 (Android, prog. jazyky, file sys, paralelní) + Mock | 4 + Mock | |
| 18 (25.5.) | **Light review ráno, zkouška odpoledne** | — | — |

**Pozn. k obhajobě 18.5.:** Cíleně je tam zařazen jen 1 konceptuální topic (SWI 22 ASP.NET teorie). Po obhajobě budeš utahaný/wired, takže tlačit na hands-on kód = recept na frustraci. SWI 22 je čistá teorie (Razor Pages, syntaxe, služby) — sednout, projít zápisky, otestovat se. Večerní ČJL Steinbeck (1992 film, 115 min) dál slouží jako pasivní dekomprese.

**Pozn. k Dni 16:** 7 topics v jeden den je tlak. Ráno Block G (UML, vzory, testing, events — všechno konceptuální, žádný kód), odpoledne lehčí H (šifrování + krypto + Git). Pokud zaostáváš, šifrování + krypto lze sloučit do 1 sezení (souvisí).

---

## Denní rytmus (vzor)

```
08:00–12:00  SWI/DAT — hlavní blok dne (deep work, tichý čas)
12:00–13:00  Oběd + pauza (žádné obrazovky pokud možno)
13:00–17:00  SWI/DAT — pokračování / praktické cvičení
17:00–18:00  Pauza, jídlo, sport
18:00–21:00  ČJL — film/audio + 15 min rozbor poté
21:00–22:00  Flashcards + zítřejší příprava
```

Tohle je *vzor*, ne pravidlo. Když mozek odmítá kód, přepni na ČJL nebo Blok G (lehčí). Když je tah, jeď dál.

---

## Claude Code workflow (vlastní zápisky)

### Adresářová struktura

```
maturita/
├── _materials/          # SUROVÝ VSTUP — školní zápisky, slidy, vzorová zadání
│   ├── swi/
│   │   ├── 01-uml/
│   │   ├── 02-algoritmus/
│   │   └── ...
│   └── dat/
│       ├── 01-html5/
│       └── ...
├── _notes/              # MOJE ZÁPISKY (generované Claude Code, pak edituji)
│   ├── swi/
│   └── dat/
├── _flashcards/         # Klíčové pojmy pro rychlé opakování
├── _practice/           # DAT praktika — reálný kód
│   ├── razor-app/
│   ├── react-app/
│   └── sql-sandbox/
├── _cjl/                # Knihy + rozbory + filmy/audio links
└── _meta/
    ├── notes-template.md   # Šablona pro generování zápisků
    └── progress.md         # Můj status (umím / neumím / k revizi)
```

### Postup per téma (~45–60 min/téma)

1. **Sběr (5 min):** všechny materiály do `_materials/swi/05-vzory/`
2. **Generace (5 min):** Claude Code prompt: *"Z materiálů ve `_materials/swi/05-vzory/` vygeneruj zápisky podle šablony `_meta/notes-template.md`. Cíl: ústní zkouška, 15 min mluvení."*
3. **Review (15 min):** přečíst, doplnit vlastní příklady, odstranit balast
4. **Flashcards (5 min):** Claude Code: *"Z `_notes/swi/05-vzory.md` udělej 8–12 flashcards (Q/A) do `_flashcards/swi-05.md`."*
5. **Test sebe (10 min):** zavřít zápisky, mluvit na hlas (na záznamník nebo zrcadlo). Co nejdu vyložit → zpět do zápisků
6. **Update progress.md:** ✅ umím / 🟡 částečně / ❌ k revizi

### Šablona zápisků (`_meta/notes-template.md`)

```markdown
# {Téma}

## Co řeknu jako první (30 s úvod)
{Jednou větou: co to je, k čemu to slouží.}

## Klíčové pojmy
{Bullet seznam s definicemi.}

## Hlubší výklad (3–5 min mluvení)
{Strukturovaně.}

## Příklady / kód
{Konkrétní ukázky.}

## Souvislosti
{Co s tím souvisí v jiných tématech.}

## Časté otázky komise
{2–4 otázky, které se mohou zeptat, + odpovědi.}
```

---

## DAT praktická příprava (kritická)

DAT má 30 min u počítače. Teorie tě nezachrání. Pravidlo:

> **Každé "praktické" DAT téma musí být v `_practice/` jako fungující kód, který umím napsat z prázdného souboru za <20 minut.**

### Praktická témata (drilovat)

- DAT 1–7 (HTML/CSS/Bootstrap/Flexbox/Grid) — postavit 5 layoutů od nuly
- DAT 16 — SQL select/join/where/group/having (sandbox SQLite)
- DAT 17 — REST API v ASP.NET (controller s GET/POST/PUT/DELETE)
- DAT 18 — Razor Pages (form + handler + redirect)
- DAT 19 — Tag Helpers (form s validací)
- DAT 21 — ORM (EF Core: model → migrace → CRUD)
- DAT 22 — React komponenty (props passing, callback)
- DAT 23 — hooks (useState counter, useEffect fetch)
- DAT 24 — Router (3 stránky, navigace)
- DAT 25 — useContext theme switcher

### Drill protokol (poslední 3 dny)

Každé téma 1× "from scratch" stopkami. Pokud >25 min → repeat zítra.

---

## ČJL track — knihy × audio/video

Strategie: **každý den 1 nová kniha** přes film/audio (7–14 dní), poslední 3–7 dní revize. Po každém filmu 10–15 min napsat vlastními slovy: děj, postavy, téma, kontext.

| # | Kniha | Doporučený zdroj |
|---|-------|------------------|
| 1 | Romeo a Julie (Shakespeare) | Film: Zeffirelli 1968 (klasika) nebo Luhrmann 1996 (modern) |
| 2 | Lakomec (Molière) | ČT: Lakomec (Werich, archiv) / divadelní záznam |
| 3 | Revizor (Gogol) | ČT divadelní záznam (Národní divadlo, archiv) |
| 4 | Obraz Doriana Graye (Wilde) | Film 2009 (Ben Barnes) + audiokniha |
| 5 | Kytice (Erben) | Film F.A. Brabec 2000 ⭐ silně doporučuju |
| 6 | Po nás ať přijde potopa (Gellner) | Audio recitace básní (YouTube) |
| 7 | Deset malých černoušků (Christie) | BBC mini-série 2015 (And Then There Were None) |
| 8 | Na západní frontě klid (Remarque) | Film Netflix 2022 ⭐ |
| 9 | O myších a lidech (Steinbeck) | Film 1992 (Sinise/Malkovich) |
| 10 | Farma zvířat (Orwell) | Animovaný film 1954 nebo TV 1999 |
| 11 | 1984 (Orwell) | Film 1984 (John Hurt) |
| 12 | Sněhulák (Nesbø) | Film 2017 (Fassbender) |
| 13 | Krysař (Dyk) | Film F.A. Brabec 2003 + audiokniha (krátká) |
| 14 | Švejk (Hašek) | Film Steklý 1956–57 (1. díl, černobílý klasik) |
| 15 | RUR (Čapek) | Rozhlasová hra ČRo + krátká, jen 1. dějství stačí |
| 16 | Bylo nás pět (Poláček) | TV seriál Smyczek 1994 ⭐ |
| 17 | Ostře sledované vlaky (Hrabal) | Film Menzel 1966 (Oscar) ⭐ |
| 18 | Spalovač mrtvol (Fuks) | Film Herz 1969 (kultovní, hutný) |
| 19 | Smrt krásných srnců (Pavel) | Film Kachyňa 1986 |
| 20 | Vyšetřování ztráty třídní knihy | Cimrmani audio/divadelní záznam (krátké, lehké, na konec) |

**Workflow per kniha:**
1. (Den N) Pustím film/audio (1,5–2,5 h, často během jídla / večer)
2. (Den N večer) 15 min napíšu rozbor podle šablony do `_cjl/{cislo}-{titul}.md`
3. (Den N+3, +7, +14) krátká revize — 5 min, jen flashcards (postavy, téma, kontext)

**Šablona ČJL rozboru:**

```markdown
# {Titul} — {Autor}

## Žánr a literárně-historický kontext
{Směr (realismus, modernismus...), období, Česká/světová.}

## Místo a čas
## Postavy (hlavní + jejich charakteristika)
## Děj (5–8 vět)
## Téma a hlavní motivy
## Kompozice, vypravěč, jazyk a styl
## Autor (krátce — život, další díla)
## K čemu to chci říct víc na zkoušce
{Co mě napadá, k čemu to vztáhnout.}
```

---

## Mock zkouška (24.5.)

- 9:00 — losování 3 témat (1 SWI, 1 DAT, 1 ČJL náhodně)
- SWI: 15 min příprava → 15 min mluvení (na záznam)
- DAT: 30 min příprava (z toho 15 min praxe na PC) → 15 min mluvení
- ČJL: 15 min příprava (s pracovním listem) → 15 min rozbor
- Odpoledne: review nahrávek, identifikace slabých míst, last-minute fixes

---

## Crisis protocol (když zaostávám)

Pokud po Bloku C (16.5.) jsem víc než 4 témata pozadu → triage:

1. **Identifikuj témata, která NEUMÍM A NEMÁM ŠANCI je dobře umět** za zbývající čas. U těch jdu na minimum: 15 min nudná tabulka pojmů, dost na to, abych něco řekl.
2. **Zaměř se na témata, která UMÍM částečně** — z 6/10 na 8/10 je rychlejší než z 0 na 6.
3. **DAT praktická > SWI teoretická** — protože DAT je u počítače a teorie tě nezachrání.
4. **Drop perfectionism on Claude Code workflow** — když nestíhám, klasické zápisky stačí.

---

## Dnes (7.5.) — setup checklist

- [ ] **Sebehodnocení 1–5** u všech 50 témat → kam patří červené tečky
- [ ] **Vytvořit adresářovou strukturu** `maturita/` (viz výše)
- [ ] **Nahrát všechny existující materiály** do `_materials/`
- [ ] **Toolchain check (DAT):**
  - [ ] .NET SDK funguje (`dotnet new webapp` projde)
  - [ ] Node.js + npm (`npx create-react-app test` projde)
  - [ ] DB engine (SQLite stačí, nebo SQL Server LocalDB)
  - [ ] Git nakonfigurovaný (jméno, email)
  - [ ] IDE (VS / VS Code / Rider)
- [ ] **Šablonu zápisků** (`_meta/notes-template.md`) podle vzoru výše
- [ ] **První pokusná generace zápisků** — vyber téma, kde máš materiály — nech Claude Code vygenerovat — zhodnoť, jestli šablona dává smysl, případně upravit
- [ ] **Začít první kapitolu Bloku A** (SWI 2 Algoritmus) — nejlehčí ze startu

Pokud se podaří 1.–5. + první šablonový pokus, dnešek je úspěch. Pokud se zvládne i začátek SWI 2 → bonus.

---

## Progress tracker (`_meta/progress.md`)

Vytvoř si tabulku, kde u každého z 50 témat držíš:
- Sebehodnocení 1–5 (před studiem)
- Status: ❌ / 🟡 / ✅
- Datum poslední revize
- Poznámka (slabá místa)

Stejně pro 20 ČJL knih.
